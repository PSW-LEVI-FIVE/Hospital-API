using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IRoomEquipmentRepository : IBaseRepository<RoomEquipment>
    {
        Task<List<RoomEquipment>> GetEquipmentByRoom(int roomId);
        int GetNumberOfUsedEquipment(int equipmentId);

    }
}