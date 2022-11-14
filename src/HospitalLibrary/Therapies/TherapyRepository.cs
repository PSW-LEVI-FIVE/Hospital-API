using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Therapies
{
    public class TherapyRepository: BaseRepository<Therapy>, ITherapyRepository
    {
        public TherapyRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}