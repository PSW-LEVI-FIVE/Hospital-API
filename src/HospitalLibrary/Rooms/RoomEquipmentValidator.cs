using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
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

    }
}
