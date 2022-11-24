using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms
{
    public class RoomEquipmentValidator : IRoomEquipmentValidator
    {
        private IUnitOfWork _unitOfWork;

        public RoomEquipmentValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public void GetAllByCombineSearchInRoomValidation(RoomEquipmentDTO roomEqipmentDTO)
        {
            
        }

        public void GetAllByNameSearchInRoom(RoomEquipmentDTO roomEqipmentDTO)
        {
            var result = _unitOfWork.RoomEquipmentRepository.GetAllByNameSearchInRoom(roomEqipmentDTO);
            if (result == null)
                throw new NotFoundException("There is no equipment with given name in desired room"); 
        }

        public void GetAllByQuantitySearchInRoom(RoomEquipmentDTO roomEqipmentDTO)
        {
            var result = _unitOfWork.RoomEquipmentRepository.GetAllByQuantitySearchInRoom(roomEqipmentDTO);
            if (result == null)
                throw new NotFoundException("There is no equipment with given quantity in desired room");
        }
    }
}
