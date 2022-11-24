using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Map
{
    public class MapFloorRepository: BaseRepository<MapFloor>, IMapFloorRepository
    {
        public MapFloorRepository(HospitalDbContext dataContext) : base(dataContext)
        {
            
        }
        
        public async Task<IEnumerable<MapFloor>> GetFloorsByBuilding(int buildingId)
        {
            return await _dataContext.MapFloors.Where(r => r.MapBuildingId == buildingId).ToListAsync();
        }
    }
}