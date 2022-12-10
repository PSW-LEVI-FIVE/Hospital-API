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
        public IEnumerable<Doctor> GetAllDoctorsWithSpecialityExceptId(int specialityId, int doctorId)
        {
            return _dataContext.Doctors.Where(doctor => doctor.Speciality.Id.Equals(specialityId) && doctor.Id != doctorId).Include( d => d.Speciality).ToList();
        }
        public async Task<IEnumerable<Doctor>> GetUnburdenedDoctors(int mostUnburdenedPatientsCount)
        {
            return await _dataContext.Doctors
                .Where(doctor => doctor.Speciality.Name
                .Equals("INTERNAL_MEDICINE"))
                .Where(doctor => doctor.Patients.Count <= mostUnburdenedPatientsCount + 2)
                .OrderBy(doctor => doctor.Patients.Count)
                .Include(doctor => doctor.Patients)
                .Include( d => d.Speciality)
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsForStepByStep(int patientId)
        {
            return await _dataContext.Doctors
                .Where(doctor => doctor.Speciality.Name.Equals("INTERNAL_MEDICINE") 
                                 || doctor.Appointments.Count(a => a.PatientId == patientId) >= 1)
                .Include( d => d.Speciality)
                .ToListAsync();
        }

        public Task<Doctor> GetDoctorByUid(string doctorUid)
        {
            return _dataContext.Doctors
                .Where(doctor => doctor.Uid.Equals(doctorUid))
                .FirstAsync();
        }

        public async Task<Doctor> GetMostUnburdenedDoctor()
        {
            return await _dataContext.Doctors
                .Where(doctor => doctor.Speciality.Name
                    .Equals("INTERNAL_MEDICINE"))
                .OrderBy(doctor => doctor.Patients.Count)
                .Include(a => a.Patients)
                .Include( d => d.Speciality)
                .FirstAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByAgeRange(DateTime dateFromAge,DateTime dateToAge)
        {
            return await _dataContext.Doctors
                .Where(doctor => doctor.Speciality.Name
                .Equals("INTERNAL_MEDICINE"))
                .OrderByDescending(doctor => doctor.Patients.Count)
                .Include(a => a.Patients
                .Where(patient => patient.BirthDate < dateFromAge && patient.BirthDate > dateToAge))
                .Include( d => d.Speciality)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Speciality>> GetAllSpecialitiesInUse()
        {
            return await _dataContext.Doctors
                .Select(d => d.Speciality)
                .Distinct()
                .ToListAsync();
        }
        
    }
}