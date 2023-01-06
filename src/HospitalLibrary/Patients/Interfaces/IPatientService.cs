using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Appointments;
using HospitalLibrary.Doctors;

namespace HospitalLibrary.Patients.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAll();
        Task<Patient> AddAllergensAndDoctorToPatient(int patientId,List<Allergen> allergens,Doctor choosenDoctor);
        Patient SearchByUid(string uid);
        Task<Patient> GetOne(int patientId);
        IEnumerable<Patient> GetMaliciousPatients(DateTime dateForMaliciousPatients);
        IEnumerable<Patient> GetBlockedPatients(DateTime dateForMaliciousPatients);

    }
    
}