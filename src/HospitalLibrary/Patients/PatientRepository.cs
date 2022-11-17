using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Patients
{
    public class PatientRepository: BaseRepository<Patient>,IPatientRepository
    {
        public PatientRepository(HospitalDbContext context): base(context) {}
        public Patient GetOneByEmail(string email)
        {
            return _dataContext.Patients.Where(p => p.Email.Equals(email)).FirstOrDefault();
        }
    }
}