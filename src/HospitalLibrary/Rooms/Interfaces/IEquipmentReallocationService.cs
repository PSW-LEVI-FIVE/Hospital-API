using HospitalLibrary.Appointments;
using HospitalLibrary.Migrations;
using HospitalLibrary.Rooms.DTOs;
using HospitalLibrary.Rooms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IEquipmentReallocationService
    {
        Task<IEnumerable<EquipmentReallocation>> GetAll();

        Task Delete(int id);

        Task<List<TimeInterval>> GetPossibleInterval(int Starting_roomId, int Destination_roomId, DateTime date, TimeSpan duration);
        Task<List<Model.RoomEquipment>> GetEquipmentByRoom(int roomId);
        Task<int> GetReservedEquipment(int equipmentId);
        Task<List<EquipmentReallocation>> GetAllPending();
        Task<List<EquipmentReallocation>> GetAllPendingForToday();
        Task<EquipmentReallocation> Create(EquipmentReallocation equipmentReallocation);
        Task InitiateReallocation(EquipmentReallocation reallocation);
    }
}
