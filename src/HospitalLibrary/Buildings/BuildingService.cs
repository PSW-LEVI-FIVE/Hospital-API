using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Buildings
{
    public class BuildingService
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
    }
}