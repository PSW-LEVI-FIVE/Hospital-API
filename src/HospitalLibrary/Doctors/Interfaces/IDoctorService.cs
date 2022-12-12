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
        Task<IEnumerable<Doctor>> GetDoctorForPatientBySpeciality(int patientId, string speciality);
        Task<Doctor> GetMostUnburdenedDoctor();
        Task<Doctor> GetDoctorByUid(string doctorUid);
        Task<IEnumerable<Doctor>> GetDoctorsByAgeRange(int fromAge, int toAge);
        Task<IEnumerable<Speciality>> GetAllSpecialities();

    }
}