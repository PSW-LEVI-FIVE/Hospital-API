using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Floors
{
    public class FloorRepository: BaseRepository<Floor>, IFloorRepository
    {
        public FloorRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}