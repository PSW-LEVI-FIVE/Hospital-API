using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Patients
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalDbContext context) : base(context) { }

<<<<<<< HEAD
        public Patient SearchByUid(string uid)
        {
            return _dataContext.Patients.FirstOrDefault(p => p.Uid.Equals(uid));
        }
        public Task<IEnumerable<Patient>> GetMaliciousPatients()
=======
        public async Task<IEnumerable<Patient>> GetMaliciousPatients()
>>>>>>> 8f11d8c (getting poetential malicious users)
        {
            return await _dataContext.Patients
                .Where(patient => patient.Appointments
                .Where(appointment => appointment.State == AppointmentState.CANCELED)
                .Count() >= 3)
                .ToListAsync();
        }
    }
}