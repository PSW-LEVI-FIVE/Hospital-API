using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Rooms
{
    public class BedService: IBedService
    {
        private IUnitOfWork _unitOfWork;

        public BedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<Bed>> GetAll()
        {
            return _unitOfWork.BedRepository.GetAll();
        }

        public IEnumerable<Bed> GetAllFreeForRoom(int roomId)
        {
            return _unitOfWork.BedRepository.GetAllFreeBedsForRoom(roomId);
        }
    }
}