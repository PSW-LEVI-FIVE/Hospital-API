using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Validators;

namespace HospitalLibrary.Patients
{
    public class PatientService: IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRegistrationValidationService _registrationValidation;

        public PatientService(IUnitOfWork unitOfWork,IPatientRegistrationValidationService registrationValidation)
        {
            _unitOfWork = unitOfWork;
            _registrationValidation = registrationValidation;
        }
        
        public Task<IEnumerable<Patient>> GetAll()
        {
            return _unitOfWork.PatientRepository.GetAll();
        }
        
        public async Task<Patient> Create(Patient patient)
        {
            await _registrationValidation.ValidatePatient(patient);
            try
            {
                _unitOfWork.PatientRepository.Add(patient);
                _unitOfWork.PatientRepository.Save();
            }
            catch(Exception  ex)
            {
                throw new BadRequestException("Uid or Email is already taken");
            }
            return patient;
        }
    }
}