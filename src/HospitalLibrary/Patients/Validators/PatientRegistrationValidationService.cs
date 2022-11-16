using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HospitalLibrary.Patients;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Shared.Validators
{
    public class PatientRegistrationValidationService: IPatientRegistrationValidationService
    {
        private IUnitOfWork _unitOfWork;
        
        public PatientRegistrationValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ValidatePatient(Patient patient)
        {
            if (await _unitOfWork.PatientRepository.GetOneByUid(patient.Uid) != null)
                throw new BadRequestException("Uid is already taken");
            if (await _unitOfWork.PatientRepository.GetOneByEmail(patient.Email) != null)
                throw new BadRequestException("Email is already taken");
        }
    }
}