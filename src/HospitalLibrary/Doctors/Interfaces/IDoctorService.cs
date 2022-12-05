using HospitalLibrary.Managers.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Doctors.Interfaces
{
    public interface IDoctorService
    {
        Doctor Create(Doctor doctor);
        Task<IEnumerable<Doctor>> GetAll();
        Task<IEnumerable<Doctor>> GetIternalMedicineDoctorsForPatientRegistration();
        Task<IEnumerable<Doctor>> GetDoctorsForStepByStep(int patientId);
        Task<Doctor> GetMostUnburdenedDoctor();
        Task<IEnumerable<Doctor>> GetDoctorsByAgeRange(int fromAge, int toAge);

    }
}