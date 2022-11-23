using HospitalLibrary.Appointments;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<EquipmentReallocation>> GetAllPending()
        {
            return await _dataContext.EquipmentReallocations
                .Where(a=> a.State==ReallocationState.PENDING)
                .ToListAsync();
        }

        public async Task<List<EquipmentReallocation>> GetAllPendingForToday()
        {
            var Date = DateTime.Now;
            return await _dataContext.EquipmentReallocations
                .Where(a => a.StartAt.Date<=Date.Date)
                .Where(a => a.State == ReallocationState.PENDING)
                .ToListAsync();
        }

        public async Task<List<TimeInterval>> GetAllRoomTakenInrevalsForDate(int roomId, DateTime date)
        {
            return await _dataContext.EquipmentReallocations
                .Where(a => a.StartingRoomId == roomId || a.DestinationRoomId == roomId)
                .Where(a => a.StartAt.Date.Equals(date.Date))
                .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                .ToListAsync();
        }

        public List<TimeInterval> GetAllRoomTakenInrevalsForDateList(int roomId, DateTime date)
        {
            return _dataContext.EquipmentReallocations
                            .Where(a => a.StartingRoomId == roomId || a.DestinationRoomId == roomId)
                            .Where(a => a.StartAt.Date.Equals(date.Date))
                            .Select(a => new TimeInterval(a.StartAt, a.EndAt))
                            .OrderBy(a => a.Start)
                            .ToList();
        }

        public Task<EquipmentReallocation> GetById(int appointmentId)
        {
            throw new NotImplementedException();
        }
    }
}
