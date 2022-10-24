using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Rooms
{
    public class RoomService: IRoomService
    {
        private IUnitOfWork _unitOfWork;


        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<Room>> GetAll()
        {
            return _unitOfWork.RoomRepository.GetAll();
        }
    }
}