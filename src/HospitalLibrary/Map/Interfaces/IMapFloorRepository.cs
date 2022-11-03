using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Map.Interfaces
{
    public interface IMapFloorRepository: IBaseRepository<MapFloor>
    {
        Task<IEnumerable<MapFloor>> GetFloorsByBuilding(int buildingId);
    }
}