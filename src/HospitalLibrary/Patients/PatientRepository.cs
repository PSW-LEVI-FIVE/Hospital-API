using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Patients
{
    public class PatientRepository: BaseRepository<Patient>,IPatientRepository
    {
        public PatientRepository(HospitalDbContext context): base(context) {}

        public Patient SearchByUid(string uid)
        {
            return _dataContext.Patients.FirstOrDefault(p => p.Uid.Equals(uid));
        }
    }
}