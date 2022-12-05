using HospitalLibrary.Consiliums.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Consiliums
{
    public class ConsiliumRepository : BaseRepository<Consilium>, IConsiliumRepository
    {
        public ConsiliumRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}