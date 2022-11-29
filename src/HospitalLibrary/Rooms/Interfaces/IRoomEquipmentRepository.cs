using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IRoomEquipmentRepository: IBaseRepository<RoomEquipment>
    {
        Task<IEnumerable<RoomEquipment>> GetAllByCombineSearchInRoom(RoomEquipmentDTO roomEquipmentDTO);
        IEnumerable<Room> CheckFloorByCombineEquipmentSearch(RoomEquipmentDTO roomEquipmentDTO);
    }
}