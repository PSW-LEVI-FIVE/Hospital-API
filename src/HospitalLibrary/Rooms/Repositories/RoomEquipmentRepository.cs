using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repositories
{
    public class RoomEquipmentRepository: BaseRepository<RoomEquipment>, IRoomEquipmentRepository
    {
        public RoomEquipmentRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public int GetNumberOfUsedEquipment(int equipmentId)
        {
            
            return _dataContext.EquipmentReallocations
                .Where(a => a.EquipmentId == equipmentId)
                .Where(a=> a.State==ReallocationState.PENDING)
                .Select(a => a.Amount).Sum();
        }

        public async Task<List<RoomEquipment>> GetEquipmentByRoom(int roomId)
        {
            return await _dataContext.RoomEquipment
                .Where(a => a.RoomId==roomId )
                .ToListAsync();
        }
    }
    
}