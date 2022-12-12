using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Doctors;

namespace HospitalLibrary.Patients.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAll();
        Task<Patient> AddAllergensAndDoctorToPatient(int patientId,List<Allergen> allergens,Doctor choosenDoctor);
        Patient SearchByUid(string uid);
        Task<Patient> GetOne(int patientId);
        Task<IEnumerable<Patient>> GetMaliciousPatients();
    }
    
}