using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Patients
{
    public class PatientRepository: BaseRepository<Patient>,IPatientRepository
    {
        public PatientRepository(HospitalDbContext context): base(context) {}
    }
}