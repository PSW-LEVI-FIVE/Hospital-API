using HospitalLibrary.Appointments;
using HospitalLibrary.Rooms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IEquipmentReallocationRepository
    {
        Task<IEnumerable<TimeInterval>> GetAllRoomTakenInrevalsForDate(int roomId, DateTime date);
        List<TimeInterval> GetAllRoomTakenInrevalsForDateList(int roomId, DateTime date);
        Task<EquipmentReallocation> GetById(int appointmentId);

    }
}
