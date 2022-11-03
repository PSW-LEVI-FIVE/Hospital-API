using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Buildings
{
    public class BuildingService: IBuildingService
    {
        private IUnitOfWork _unitOfWork;


        public BuildingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<Building>> GetAll()
        {
            return _unitOfWork.BuildingRepository.GetAll();
        }

        public Building UpdateBuildingData(Building building)
        {
            _unitOfWork.BuildingRepository.Update(building);
            _unitOfWork.BuildingRepository.Save();
            return building;
        }
    }
}