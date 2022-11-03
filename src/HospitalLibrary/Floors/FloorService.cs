using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Floors
{
    public class FloorService: IFloorService
    {
        private IUnitOfWork _unitOfWork;


        public FloorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<Floor>> GetAll()
        {
            return _unitOfWork.FloorRepository.GetAll();
        }

        public Floor Update(Floor floor)
        {
            _unitOfWork.FloorRepository.Update(floor);
            _unitOfWork.FloorRepository.Save();
            return floor;
        }
    }
}