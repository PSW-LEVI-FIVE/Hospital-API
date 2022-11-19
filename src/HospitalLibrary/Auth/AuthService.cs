using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.User.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Interfaces;

namespace HospitalLibrary.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRegistrationValidationService _registrationValidation;
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;
        private readonly IUnitOfWork _unitOfWork;
        
        public AuthService(IUnitOfWork unitOfWork,IRegistrationValidationService registrationValidation,IUserService userService,IPatientService patientService)
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
    }
}