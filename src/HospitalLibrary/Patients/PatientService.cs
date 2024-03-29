﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Appointments;
using HospitalLibrary.Doctors;
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
        
        public async Task<Patient> GetOne(int patientId)
        {
            return _unitOfWork.PatientRepository.GetOne(patientId);
        }

        public async Task<Patient> AddAllergensAndDoctorToPatient(int patientId, List<Allergen> allergens, Doctor choosenDoctor)
        {
            Patient patient = _unitOfWork.PatientRepository.GetOne(patientId);
            patient.Allergens = allergens;
            patient.ChoosenDoctor = choosenDoctor;
            _unitOfWork.PatientRepository.Update(patient);
            _unitOfWork.PatientRepository.Save();
            return patient;
        }
        public Patient SearchByUid(string uid)
        {
            return _unitOfWork.PatientRepository.SearchByUid(uid);
        }
        public IEnumerable<Patient> GetMaliciousPatients(DateTime dateForMaliciousPatients)
        {
            return _unitOfWork.PatientRepository.GetMaliciousPatients(dateForMaliciousPatients).Result;
        }

        public IEnumerable<Patient> GetBlockedPatients(DateTime dateForMaliciousPatients)
        {

            return _unitOfWork.PatientRepository.GetBlockedPatients(dateForMaliciousPatients).Result;
        }

    }
}