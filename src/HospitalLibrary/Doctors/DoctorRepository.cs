using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Doctors
{
    public class DoctorRepository: BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HospitalDbContext context): base(context) {}
    }
}