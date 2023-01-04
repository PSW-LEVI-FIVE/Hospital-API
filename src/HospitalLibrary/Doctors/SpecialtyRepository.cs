using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Doctors
{
    public class SpecialtyRepository: BaseRepository<Speciality>, ISpecialtyRepository
    {
        public SpecialtyRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}