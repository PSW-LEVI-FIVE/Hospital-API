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

        public async Task<List<EquipmentReallocation>> GetAllForRoom(int roomid)
        {
          return await _dataContext.EquipmentReallocations
            .Where(a => a.StartingRoomId == roomid || a.DestinationRoomId == roomid)
            .ToListAsync();
        }

        public async Task<List<EquipmentReallocation>> GetAllPendingForRoomInTimeInterval(int roomId,
          TimeInterval timeInterval)
        {
          return await _dataContext.EquipmentReallocations
            .Where(a => a.StartingRoomId == roomId || a.DestinationRoomId == roomId)
            .Where(a =>
              timeInterval.Start.Date.CompareTo(a.StartAt.Date) <= 0
              || timeInterval.End.Date.CompareTo(a.StartAt.Date) > 0)
            .ToListAsync();
        }

    public async Task<List<EquipmentReallocation>> GetAllPending()
        {
            return await _dataContext.EquipmentReallocations
                .Where(a=> a.state==ReallocationState.PENDING)
                .ToListAsync();
        }
        private int maxID()
        {
         return _dataContext.RoomEquipment.OrderByDescending(a => a.Id).FirstOrDefault().Id;
        }

        public async Task<List<EquipmentReallocation>> GetAllPendingForToday()
        {
            var Date = DateTime.Now;
            return await _dataContext.EquipmentReallocations
                .Where(a => a.StartAt.Date<=Date.Date)
                .Where(a => a.state == ReallocationState.PENDING)
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


    }
}
