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
        Task<IEnumerable<Doctor>> GetDoctorForPatientBySpeciality(int patientId,string speciality);
        Task<Doctor> GetDoctorByUid(string doctorUid);
        Task<Doctor> GetMostUnburdenedDoctor();
        IEnumerable<Doctor> GetDoctorsForDate(List<int> doctors, DateTime date);
        public Task<IEnumerable<Doctor>> GetDoctorsByAgeRange(DateTime dateFromAge, DateTime dateToAge);
        Task<IEnumerable<Speciality>> GetAllSpecialitiesInUse();
    }
}