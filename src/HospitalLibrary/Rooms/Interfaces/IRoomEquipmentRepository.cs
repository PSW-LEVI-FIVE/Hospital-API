using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IRoomEquipmentRepository: IBaseRepository<RoomEquipment>
    {
        Task<IEnumerable<RoomEquipment>> GetAllByNameSearchInRoom(RoomEquipmentDTO roomEquipmentDTO);
        Task<IEnumerable<RoomEquipment>> GetAllByQuantitySearchInRoom(RoomEquipmentDTO roomEquipmentDTO);
        Task<IEnumerable<RoomEquipment>> GetAllByCombineSearchInRoom(RoomEquipmentDTO roomEquipmentDTO);
        bool checkFloorByEquipmentName(IEnumerable<RoomEquipment> roomEquipment, RoomEquipmentDTO roomEquipmentDTO);
        bool checkFloorByEquipmentQuantity(IEnumerable<RoomEquipment> roomEquipment, RoomEquipmentDTO roomEquipmentDTO);
        bool checkFloorByCombineEquipmentSearch(IEnumerable<RoomEquipment> roomEquipment, RoomEquipmentDTO roomEquipmentDTO);
    }
}