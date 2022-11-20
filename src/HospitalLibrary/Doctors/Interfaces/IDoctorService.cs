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
        public Task<IEnumerable<Doctor>> GetDoctorsByAgeRange(int fromAge, int toAge);

    }
}