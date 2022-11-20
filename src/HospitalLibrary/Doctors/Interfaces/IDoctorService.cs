using HospitalLibrary.Managers.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Doctors.Interfaces
{
    public interface IDoctorService
    {
        Doctor Create(Doctor doctor);
        Task<IEnumerable<Doctor>> GetAll();
<<<<<<< HEAD
        Task<IEnumerable<Doctor>> GetIternalMedicineDoctorsForPatientRegistration();
=======
        public IEnumerable<DoctorWithPopularityDTO> GetMostPopularDoctorByAgeRange(int fromAge = 0, int toAge = 666, bool onlyMostPopularDoctors = false);
>>>>>>> 039b3a0 (rebasing)
    }
}