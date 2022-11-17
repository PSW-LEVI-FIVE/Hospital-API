using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Validators;

namespace HospitalLibrary.Patients
{
    public class PatientService: IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<Patient>> GetAll()
        {
            return _unitOfWork.PatientRepository.GetAll();
        }
        public async Task<Patient> Create(Patient patient)
        {
            if (_unitOfWork.PersonRepository.GetOneByEmail(patient.Email) != null)
                throw new BadRequestException("Email is already taken"); 
            else if(_unitOfWork.PersonRepository.GetOneByUid(patient.Uid) != null)
                throw new BadRequestException("Uid is already taken");
            _unitOfWork.PatientRepository.Add(patient);
            _unitOfWork.PatientRepository.Save();
            return _unitOfWork.PatientRepository.GetOneByEmail(patient.Email);
        }
    }
}