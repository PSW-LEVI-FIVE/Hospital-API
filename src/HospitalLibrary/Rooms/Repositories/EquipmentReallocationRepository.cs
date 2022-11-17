using HospitalLibrary.Appointments;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Repositories
{
    public class EquipmentReallocationRepository : BaseRepository<EquipmentReallocation>, IEquipmentReallocationRepository
    {
        public EquipmentReallocationRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<TimeInterval>> GetAllRoomTakenInrevalsForDate(int roomId, DateTime date)
        {
            return await _dataContext.EquipmentReallocations
                .Where(a => a.RoomId == roomId)
                .Where(a => a.StartAt.Date.Equals(date.Date))
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .OrderBy(a =>a.Start)
                .ToListAsync();
        }

        public Task<EquipmentReallocation> GetById(int appointmentId)
        {
            throw new NotImplementedException();
        }
    }
}
