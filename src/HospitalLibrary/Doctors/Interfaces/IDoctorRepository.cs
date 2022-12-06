using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Doctors.Interfaces
{
    public interface IDoctorRepository: IBaseRepository<Doctor>
    {
        IEnumerable<Doctor> GetAllDoctorsWithSpecialityExceptId(int specialityId, int doctorId);
        Task<IEnumerable<Doctor>> GetUnburdenedDoctors(int mostUnburdenedPatientsCount);
        Task<IEnumerable<Doctor>> GetDoctorsForStepByStep(int patientId);
        Task<Doctor> GetMostUnburdenedDoctor();
        public Task<IEnumerable<Doctor>> GetDoctorsByAgeRange(DateTime dateFromAge, DateTime dateToAge);
    }
}