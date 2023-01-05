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

        public void CreateEquipment(int destinationRoomId, int amount, RoomEquipment equipment)
        {
            _unitOfWork.RoomEquipmentRepository.Add(new RoomEquipment(_unitOfWork.RoomEquipmentRepository.GetHighestId() + 1,
                amount, equipment.Name, destinationRoomId));
            _unitOfWork.RoomEquipmentRepository.Save();
        }
        public void UpdateEquipment(RoomEquipment realEq)
        {
            _unitOfWork.RoomEquipmentRepository.Update(realEq);
            _unitOfWork.RoomEquipmentRepository.Save();
        }

        public void DeleteEquipment(RoomEquipment realEq)
        {
            _unitOfWork.RoomEquipmentRepository.Delete(realEq);
            _unitOfWork.RoomEquipmentRepository.Save();
        }
        
        public Task<IEnumerable<RoomEquipment>> SearchEquipmentInRoom(RoomEquipmentDTO roomEquipmentDTO)
        {
            return  _unitOfWork.RoomEquipmentRepository.GetAllByCombineSearchInRoom(roomEquipmentDTO);  
        }

        public IEnumerable<Room> SearchRoomsByFloorContainingEquipment(RoomEquipmentDTO roomEquipmentDTO)
        {
            var rooms = _unitOfWork.RoomEquipmentRepository.CheckFloorByCombineEquipmentSearch(roomEquipmentDTO);
            List<Room> result = new List<Room>();

            foreach(Room room in rooms)
            {
                if (!room.RoomEquipment.Count().Equals(0))
                    result.Add(room);

            }
            return result.AsEnumerable<Room>();
        }
    }
}
