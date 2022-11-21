using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Map.Interfaces
{
    public interface IMapService
    {
        Task<IEnumerable<MapBuilding>> GetAllBuildings();
        Task<IEnumerable<MapFloor>> GetFloorsByBuilding(int buildingId);
        Task<IEnumerable<MapRoom>> GetRoomsByFloor(int floorId);
        MapRoom CreateRoom(MapRoom room);
        MapFloor CreateFloor(MapFloor floor);
        MapFloor GetFloorById(int floorId);
        MapBuilding GetBuildingById(int buildingId);
    }
}