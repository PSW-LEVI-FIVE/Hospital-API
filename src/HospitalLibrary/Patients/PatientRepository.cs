using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Patients
{
    public class PatientRepository: BaseRepository<Patient>,IPatientRepository
    {
        public PatientRepository(HospitalDbContext context): base(context) {}
    }
}