using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users.Interfaces;

namespace HospitalLibrary.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRegistrationValidationService _registrationValidation;
        private IUnitOfWork _unitOfWork;
        
        public AuthService(IUnitOfWork unitOfWork,IRegistrationValidationService registrationValidation)
        {
            _unitOfWork = unitOfWork;
            _registrationValidation = registrationValidation;
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
            foreach (Doctor doctor in await _unitOfWork.DoctorRepository
                         .GetTwoIternalMedicineDoctorsAscendingByPatientNumber())
            {
                if (doctor.Uid.Equals(doctorUid))
                    return doctor;
            }
            throw new BadRequestException("Doctor doesnt exist or not valid!");
        }
        public async Task<Users.User> RegisterPatient(Users.User user,List<AllergenDTO> allergens,string doctorUid)
        {
            await _registrationValidation.ValidatePatientRegistration(user);
            List<Allergen> patientsAllergens = await GetPatientsAllergens(allergens);
            Doctor choosenDoctor = await GetPatientsDoctor(doctorUid);
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.UserRepository.Save();
            
            Patient createdPatient = _unitOfWork.PatientRepository.GetOne(user.Id);
            createdPatient.Allergens = patientsAllergens;
            createdPatient.ChoosenDoctor = choosenDoctor;
            _unitOfWork.PatientRepository.Update(createdPatient);
            _unitOfWork.PatientRepository.Save();
            return user;
        }
    }
}