using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
>>>>>>> 1773e5b (Refactored jwt)
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using HospitalLibrary.Users.Interfaces;
using Microsoft.AspNetCore.Http;
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
	private IConfiguration _config;
        
        public AuthService(IUnitOfWork unitOfWork,IRegistrationValidationService registrationValidation,IUserService userService,IPatientService patientService,IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _registrationValidation = registrationValidation;
            _userService = userService;
            _patientService = patientService;
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
        private async Task<Doctor> GetPatientsDoctor(string doctorUid)
        {
            foreach (Doctor doctor in await _unitOfWork.DoctorRepository.GetTwoUnburdenedDoctors())
            {
                if (doctor.Uid.Equals(doctorUid))
                    return doctor;
            }
            throw new BadRequestException("Doctor doesnt exist or not valid!");
        }
        public async Task<PatientDTO> RegisterPatient(CreatePatientDTO createPatientDTO)
        {
            Users.User user = createPatientDTO.MapUserToModel();
            user.Person = createPatientDTO.MapPatientToModel();
            
            await _registrationValidation.ValidatePatientRegistration(user);
            
            List<Allergen> patientsAllergens = await GetPatientsAllergens(createPatientDTO.Allergens);
            Doctor choosenDoctor = await GetPatientsDoctor(createPatientDTO.DoctorUid);
            
            await _userService.Create(user);
            await _patientService.AddAllergensAndDoctorToPatient(user.Id,patientsAllergens, choosenDoctor);
            
            return new PatientDTO(createPatientDTO);
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
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
                claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserDTO Authenticate(UserDTO userDto)
        {
            var currentUser = UserExist(userDto.Username, userDto.Password);
            if (currentUser != null)
            {
                return new UserDTO(currentUser.Username,currentUser.Password,currentUser.Role);
            }

            return null;

        }
        
        
        
    }
}