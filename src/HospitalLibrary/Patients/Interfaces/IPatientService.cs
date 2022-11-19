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
    }
}