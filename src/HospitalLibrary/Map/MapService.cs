using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HospitalLibrary.Map
{
    public class MapService: IMapService
    {
        private IUnitOfWork _unitOfWork;


        public MapService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<MapBuilding>> GetAllBuildings()
        {
            return _unitOfWork.MapBuildingRepository.GetAll();
        }

        public Task<IEnumerable<MapFloor>> GetFloorsByBuilding(int buildingId)
        {
            return _unitOfWork.MapFloorRepository.GetFloorsByBuilding(buildingId);
        }

        public Task<IEnumerable<MapRoom>> GetRoomsByFloor(int floorId)
        {
            return _unitOfWork.MapRoomRepository.GetRoomsByFloor(floorId);
        }

        public MapRoom CreateRoom(MapRoom room)
        {
           _unitOfWork.MapRoomRepository.Add(room);
           _unitOfWork.MapRoomRepository.Save();
           return room;
        }

        public MapFloor CreateFloor(MapFloor floor)
        {
            _unitOfWork.MapFloorRepository.Add(floor);
            _unitOfWork.MapFloorRepository.Save();
            return floor;
        }

        public MapBuilding CreateBuilding(MapBuilding building)
        {
            _unitOfWork.MapBuildingRepository.Add(building);
            _unitOfWork.MapBuildingRepository.Save();
            return building;
        }

        public MapFloor GetFloorById(int floorId)
        {
            return _unitOfWork.MapFloorRepository.GetOne(floorId);
        }

        public MapBuilding GetBuildingById(int buildingId)
        {
            return _unitOfWork.MapBuildingRepository.GetOne(buildingId);
        }
        
    }
}