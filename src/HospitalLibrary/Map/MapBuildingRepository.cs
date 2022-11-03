using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Map
{
    public class MapBuildingRepository: BaseRepository<MapBuilding>, IMapBuildingRepository
    {
        public MapBuildingRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}