using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IRoomEquipmentService
    {
        void UpdateEquipment(RoomEquipment realEq);
        void DeleteEquipment(RoomEquipment realEq);

        void CreateEquipment(int destinationRoomId, int amount, RoomEquipment equipment);
        Task<IEnumerable<RoomEquipment>> SearchEquipmentInRoom(RoomEquipmentDTO roomEquipmentDTO);
        IEnumerable<Room> SearchRoomsByFloorContainingEquipment(RoomEquipmentDTO roomEquipmentDTO);
    }
}
