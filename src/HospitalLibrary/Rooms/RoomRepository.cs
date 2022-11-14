﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms
{
    public class RoomRepository: BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<Room>> FindAllByFloor(int floor)
        {
            return await _dataContext.Rooms.Where(r => r.FloorId == floor).ToListAsync();
        }
    }
}