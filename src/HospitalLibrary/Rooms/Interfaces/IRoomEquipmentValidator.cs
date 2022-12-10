using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Dtos;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IRoomEquipmentValidator
    {
        void GetAllByCombineSearchInRoomValidation(RoomEquipmentDTO roomEqipmentDTO);
    }
}
