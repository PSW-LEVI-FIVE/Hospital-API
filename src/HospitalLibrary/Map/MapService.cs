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
    }
}