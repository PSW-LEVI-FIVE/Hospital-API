using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Auth.Interfaces;
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
        
        public async Task<Users.User> RegisterPatient(Users.User user,List<AllergenDTO> allergens)
        {
            await _registrationValidation.ValidatePatientRegistration(user);
            List<Allergen> patientsAllergens = new List<Allergen>();
            foreach (AllergenDTO allergenDTO in allergens)
            {
                Allergen allergen = await _unitOfWork.AllergenRepository.GetOneByName(allergenDTO.Name);
                if (allergen == null) 
                    throw new BadRequestException("Allergen doesnt exist!");
                patientsAllergens.Add(allergen);
            }
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.UserRepository.Save();
            
            Patient createdPatient = _unitOfWork.PatientRepository.GetOne(user.Id);
            createdPatient.Allergens = patientsAllergens;
            _unitOfWork.PatientRepository.Update(createdPatient);
            _unitOfWork.PatientRepository.Save();
            return user;
        }
    }
}