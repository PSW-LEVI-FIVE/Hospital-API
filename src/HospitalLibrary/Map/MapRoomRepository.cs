﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Map
{
    public class MapRoomRepository: BaseRepository<MapRoom>, IMapRoomRepository
    {
        public MapRoomRepository(HospitalDbContext dataContext) : base(dataContext)
        {
            
        }
        
        public async Task<IEnumerable<MapRoom>> GetRoomsByFloor(int floorId)
        {
            return await _dataContext.MapRooms.Where(r => r.Room.FloorId == floorId).ToListAsync();
        }
    }
}