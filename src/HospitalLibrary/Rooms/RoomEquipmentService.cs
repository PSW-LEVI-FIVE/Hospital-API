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
            switch (checkSearchInputRoom(roomEquipmentDTO))
            {
                case 0: 
                    return  _unitOfWork.RoomEquipmentRepository.GetAllByCombineSearchInRoom(roomEquipmentDTO);
                case 1:
                    return _unitOfWork.RoomEquipmentRepository.GetAllByNameSearchInRoom(roomEquipmentDTO);
                case 2:
                    return _unitOfWork.RoomEquipmentRepository.GetAllByQuantitySearchInRoom(roomEquipmentDTO);
                default: 
                    return _unitOfWork.RoomRepository.GetAllEquipmentbyRoom(roomEquipmentDTO.RoomId);
            }  
        }

        public IEnumerable<Room> searchEquipmentOnFloor(RoomEquipmentDTO roomEquipmentDTO)
        {
            var rooms =  _unitOfWork.RoomRepository.FindAllByFloor(roomEquipmentDTO.RoomId);
            List<Room> result = new List<Room>();
            foreach (var room in rooms.Result)
            {
                var equipment = _unitOfWork.RoomRepository.GetAllEquipmentbyRoom(room.Id);
                if (equipment.Result != null)
                {
                   if(checkSearchInputFloor(roomEquipmentDTO, equipment))
                    {
                        
                        result.Add(room);
                    }
                }
            }
           
            IEnumerable<Room> resultList = result.AsEnumerable<Room>();
            return resultList;
        }

        public int checkSearchInputRoom(RoomEquipmentDTO roomEquipmentDTO)
        {
            var status = 0;
            if (roomEquipmentDTO.Name != "0" && roomEquipmentDTO.Quantity > 0)
            {
                return status;
            }

            else if (roomEquipmentDTO.Name != "0")
            {
                return status + 1;
            }
            else if(roomEquipmentDTO.Quantity > 0)
            {
                return status + 2;
            }
           else
            {
                return status + 3;
            }
        }

        public bool checkSearchInputFloor(RoomEquipmentDTO roomEquipmentDTO, Task<IEnumerable<RoomEquipment>> roomEquipment)
        {
            switch (checkSearchInputRoom(roomEquipmentDTO))
            {
                case 0:
                     return _unitOfWork.RoomEquipmentRepository.checkFloorByCombineEquipmentSearch(roomEquipment, roomEquipmentDTO);
                case 1:
                    return _unitOfWork.RoomEquipmentRepository.checkFloorByEquipmentName(roomEquipment, roomEquipmentDTO);
                case 2:
                    return _unitOfWork.RoomEquipmentRepository.checkFloorByEquipmentQuantity(roomEquipment, roomEquipmentDTO);
                default: return false;
            }
        }
    }
}
