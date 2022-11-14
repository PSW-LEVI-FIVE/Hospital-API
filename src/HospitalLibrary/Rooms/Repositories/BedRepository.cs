using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Rooms.Repositories
{
    public class BedRepository: BaseRepository<Bed>, IBedRepository
    {
        public BedRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}