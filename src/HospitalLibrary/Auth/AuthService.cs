using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using HospitalLibrary.Users.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HospitalLibrary.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRegistrationValidationService _registrationValidation;
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;
        private readonly IUnitOfWork _unitOfWork;
	    private readonly IConfiguration _config;
        
        public AuthService(IUnitOfWork unitOfWork,IRegistrationValidationService registrationValidation,IUserService userService,IPatientService patientService,IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _registrationValidation = registrationValidation;
            _userService = userService;
            _patientService = patientService;
            _config = config;
        }
        public async Task<List<Allergen>> GetPatientsAllergens(List<AllergenDTO> allergenDTOs)
        {
            List<Allergen> allergens = new List<Allergen>();
            foreach (AllergenDTO allergenDTO in allergenDTOs)
            {
                Allergen allergen = await _unitOfWork.AllergenRepository.GetOneByName(allergenDTO.Name);
                if (allergen == null)
                    throw new BadRequestException("Allergen doesnt exist!");
                allergens.Add(allergen);
            }
            return allergens;
        }
        public async Task<Doctor> GetPatientsDoctor(string doctorUid)
        {
            Doctor mostUnburdened = await _unitOfWork.DoctorRepository.GetMostUnburdenedDoctor();
            foreach (Doctor doctor in await _unitOfWork.DoctorRepository.GetUnburdenedDoctors(mostUnburdened.Patients.Count))
            {
                if (doctor.Uid.Equals(doctorUid))
                    return doctor;
            }
            throw new BadRequestException("Doctor doesnt exist or not valid!");
        }
        private async Task<string> CreateActivationCode()
        {
            string activationCode = Guid.NewGuid().ToString();
            bool codeUnique = _userService.IsCodeUnique(activationCode);
            if (codeUnique) 
                return activationCode;
            return await CreateActivationCode();
        }
        public async Task<PatientDTO> RegisterPatient(CreatePatientDTO createPatientDTO)
        {
            Users.User user = createPatientDTO.MapUserToModel();
            user.Person = createPatientDTO.MapPatientToModel();
            
            await _registrationValidation.ValidatePatientRegistration(user);
            
            List<Allergen> patientsAllergens = await GetPatientsAllergens(createPatientDTO.Allergens);
            Doctor choosenDoctor = await GetPatientsDoctor(createPatientDTO.DoctorUid);
            user.ActivationCode = await CreateActivationCode();
            
            await _userService.Create(user);
            await _patientService.AddAllergensAndDoctorToPatient(user.Id,patientsAllergens, choosenDoctor);
            
            PatientDTO registeredPatient = new PatientDTO(createPatientDTO)
            {
                ActivationCode = user.ActivationCode
            };

            return registeredPatient;
        }

        public async Task<PatientDTO> ActivateAccount(string code)
        {
            Users.User user = await _userService.GetOneByCode(code);
            if (user == null)
                throw new BadRequestException("Activation code not valid!");
            await _userService.ActivateAccount(user,code);
            return new PatientDTO(user);
        }

        public Users.User UserExist(string username, string password)
        {
            return _unitOfWork.UserRepository.UserExist(username, password);
        }
        public string Generate(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
                claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserDTO Authenticate(UserDTO userDto)
        {
            var currentUser = UserExist(userDto.Username, userDto.Password);
            if (currentUser != null)
            {
                if (currentUser.ActiveStatus == ActiveStatus.Active)
                {
                    return new UserDTO(currentUser.Username, currentUser.Password, currentUser.Role, currentUser.Id);
                }
            }
            return null;
        }
    }
}