using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Users;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using HospitalLibrary.Appointments;

namespace HospitalLibrary.Patients
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalDbContext context) : base(context) { }

        public Patient SearchByUid(string uid)
        {
            return _dataContext.Patients.FirstOrDefault(p => p.Uid.Equals(uid));
        }
        public async Task<IEnumerable<Patient>> GetMaliciousPatients(DateTime dateForMaliciousPatients)
        {
            IEnumerable<Patient> potentialMaliciousPatients = await _dataContext.Patients
                .Where(patient => patient.Appointments
                .Where(appointment => appointment.State == AppointmentState.CANCELED && 
                    appointment.StartAt > dateForMaliciousPatients)
                .Count() >= 3)
                .Include(patient => patient.Appointments
                .Where (appointment => appointment.State == AppointmentState.CANCELED))
                .ToListAsync();
            foreach(Users.User user in _dataContext.Users.Where(user => user.Blocked == true))
            {
                potentialMaliciousPatients.Append(_dataContext.Patients
                    .Where(patient => patient.Id == user.Id)
                    .Include(patient => patient.Appointments
                    .Where(appointment => appointment.State == AppointmentState.CANCELED)).First());
            }
            return potentialMaliciousPatients;
        }
    }
}