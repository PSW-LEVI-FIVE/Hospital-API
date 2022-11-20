using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using System;
using HospitalLibrary.Patients;

namespace HospitalLibrary.Doctors
{
    public class DoctorRepository: BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HospitalDbContext context): base(context) {}
        public IEnumerable<Doctor> GetAllDoctorsWithSpecialityExceptId(SpecialtyType specialtyType, int doctorId)
        {
            return _dataContext.Doctors.Where(doctor => doctor.SpecialtyType.Equals(specialtyType) && doctor.Id != doctorId).ToList();
        }
        public async Task<IEnumerable<Doctor>> GetTwoUnburdenedDoctors()
        {
            return await _dataContext.Doctors
                .Where(doctor => doctor.SpecialtyType
                .Equals(SpecialtyType.ITERNAL_MEDICINE))
                .OrderBy(doctor => doctor.Patients.Count)
                .Include(a => a.Patients)
                .Take(2).ToListAsync();
        }
        
        public async Task<IEnumerable<Doctor>> GetDoctorsByAgeRange(DateTime dateFromAge,DateTime dateToAge)
        {
            return await _dataContext.Doctors
                .Where(doctor => doctor.SpecialtyType
                .Equals(SpecialtyType.ITERNAL_MEDICINE))
                .OrderByDescending(doctor => doctor.Patients.Count)
                .Include(a => a.Patients
                .Where(patient => patient.BirthDate < dateFromAge && patient.BirthDate > dateToAge))
                .ToListAsync();
        }
    }
}