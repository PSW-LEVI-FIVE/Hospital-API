using HospitalLibrary.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Patients.Interfaces
{
    public interface IPatientRepository: IBaseRepository<Patient>
    {
        public Patient SearchByUid(string uid);
        public Task<IEnumerable<Patient>> GetMaliciousPatients();
    }
}