using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
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

        public Room Update(Room room)
        {
            _unitOfWork.RoomRepository.Update(room);
            _unitOfWork.RoomRepository.Save();
            return room;
        }

        public Room GetOne(int key)
        {
            return _unitOfWork.RoomRepository.GetOne(key);
        }
    }
}