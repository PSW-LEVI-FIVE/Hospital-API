using HospitalLibrary.Shared.Interfaces;
using System;
using System.Collections.Generic;
using HospitalLibrary.Users;
using System.Threading.Tasks;

namespace HospitalLibrary.Patients.Interfaces
{
    public interface IPatientRepository: IBaseRepository<Patient>
    {
        public Patient SearchByUid(string uid);
        public Task<IEnumerable<Patient>> GetMaliciousPatients(DateTime dateForMaliciousPatients);
        public Task<IEnumerable<Patient>> GetBlockedPatients(DateTime dateForMaliciousPatients);
    }
}