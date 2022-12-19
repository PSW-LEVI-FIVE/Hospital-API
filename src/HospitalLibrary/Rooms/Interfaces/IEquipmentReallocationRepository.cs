using HospitalLibrary.Appointments;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IEquipmentReallocationRepository: IBaseRepository<EquipmentReallocation>
    { 
        Task<List<TimeInterval>> GetAllRoomTakenInrevalsForDate(int? roomId, DateTime date);
        Task<List<EquipmentReallocation>> GetAllPending();
        Task<List<EquipmentReallocation>> GetAllPendingForDate(DateTime date);
        Task<List<EquipmentReallocation>> GetAllPendingForToday();
        Task<EquipmentReallocation> GetLastPendingForDay(DateTime date, int roomid);
        Task<EquipmentReallocation> GetFirstPendingForDay(DateTime date, int roomid);
        Task<List<EquipmentReallocation>> GetAllPendingForDateAndRoom(DateTime date, int roomid);
        Task<List<EquipmentReallocation>> GetAllForRoom(int roomid);

    }
}
