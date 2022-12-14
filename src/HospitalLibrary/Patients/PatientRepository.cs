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
            return await _dataContext.Patients
                .Where(patient => patient.Appointments
                .Where(appointment => appointment.State == AppointmentState.DELETED && 
                    appointment.StartAt > dateForMaliciousPatients)
                .Count() >= 3)
                .Include(patient => patient.Appointments
                .Where (appointment => appointment.State == AppointmentState.DELETED &&
                        appointment.StartAt > dateForMaliciousPatients))
                .ToListAsync();
        }
        public async Task<IEnumerable<Patient>> GetBlockedPatients(DateTime dateForMaliciousPatients)
        {
            List<Patient> potentialMaliciousPatients = new List<Patient>();
            IEnumerable<Users.User> blockedUsers = await _dataContext.Users.Where(user => user.Blocked == true).ToListAsync();
            foreach (Users.User user in blockedUsers)
            {
                potentialMaliciousPatients.Add(_dataContext.Patients
                    .Where(patient => patient.Id == user.Id)
                    .Include(patient => patient.Appointments
                    .Where(appointment => appointment.State == AppointmentState.DELETED &&
                        appointment.StartAt > dateForMaliciousPatients))
                    .First());
            }
            return potentialMaliciousPatients.AsEnumerable();
        }

    }
}