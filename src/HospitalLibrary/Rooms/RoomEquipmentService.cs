using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms
{
    public class RoomEquipmentService: IRoomEquipmentService
    {
        private IUnitOfWork _unitOfWork;
        private IRoomEquipmentValidator  _roomEquipmentValidator;

        public RoomEquipmentService(IUnitOfWork unitOfWork, IRoomEquipmentValidator roomEquipmentValidator)
        {
            _unitOfWork = unitOfWork;
            _roomEquipmentValidator = roomEquipmentValidator;
        }

        public Task<IEnumerable<RoomEquipment>> searchEquipmentInRoom(RoomEquipmentDTO roomEquipmentDTO)
        {
            return  _unitOfWork.RoomEquipmentRepository.GetAllByCombineSearchInRoom(roomEquipmentDTO);  
        }

        public IEnumerable<Room> searchRoomsByFloorContainingEquipment(RoomEquipmentDTO roomEquipmentDTO)
        {
            var rooms = _unitOfWork.RoomEquipmentRepository.CheckFloorByCombineEquipmentSearch(roomEquipmentDTO);
            List<Room> result = new List<Room>();

            foreach(Room room in rooms)
            {
                if (!room.RoomEquipment.Count().Equals(0))
                    result.Add(room);

            }
            IEnumerable<Room> resultList = result.AsEnumerable<Room>();
            return resultList;
        }




    }
}
