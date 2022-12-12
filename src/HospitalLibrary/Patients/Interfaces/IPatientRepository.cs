using HospitalLibrary.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Patients.Interfaces
{
    public interface IPatientRepository: IBaseRepository<Patient>
    {
<<<<<<< HEAD
        public Patient SearchByUid(string uid);
        public Task<IEnumerable<Patient>> GetMaliciousPatients();
=======
        Task<IEnumerable<Patient>> GetMaliciousPatients();
>>>>>>> 8f11d8c (getting poetential malicious users)
    }
}