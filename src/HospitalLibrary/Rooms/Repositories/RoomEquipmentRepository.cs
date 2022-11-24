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
                .Where(a=> a.state==ReallocationState.PENDING)
                .Select(a => a.amount).Sum();
        }
        public async Task<RoomEquipment> GetEquipmentByRoomAndName(int roomId,string name)
        {
            if(!_dataContext.RoomEquipment.Any(a => a.RoomId == roomId && a.Name == name))
                return null;
            return await _dataContext.RoomEquipment
                .Where(a => a.RoomId == roomId)
                .Where(a=>a.Name==name)
                .SingleAsync();
            ;
        }
        public async Task<List<RoomEquipment>> GetEquipmentByRoom(int roomId)
        {
            return await _dataContext.RoomEquipment
                .Where(a => a.RoomId==roomId )
                .ToListAsync();
        }
    }
    
}