using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Patients.Interfaces
{
    public interface IPatientRepository: IBaseRepository<Patient>
    {
        public Patient SearchByUid(string uid);
    }
}