using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Patients.Interfaces
{
    public interface IPatientRepository: IBaseRepository<Patient>
    {
        Task<Patient> GetOneByUid(string uid);
        Task<Patient> GetOneByEmail(string email);
    }
}