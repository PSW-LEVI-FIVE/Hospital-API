using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repositories
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

        public async Task<IEnumerable<Room>> SearchByName(RoomSearchDTO roomSearchDTO,int id)
        {
            return await _dataContext.Rooms.Where(r => r.FloorId == id)
                                            .Where(r => r.RoomNumber.Contains(roomSearchDTO.RoomName)).ToListAsync();
        }

        public async Task<IEnumerable<Room>> SearchByType(RoomSearchDTO roomSearchDTO,int id)
        {
            Enum.TryParse(roomSearchDTO.RoomType, true, out RoomType roomType);
            return await _dataContext.Rooms.Where(r => r.FloorId == id)
                                            .Where(r => r.RoomType==(roomType)).ToListAsync();
        }

        public async Task<IEnumerable<Room>> SearchByTypeAndName(RoomSearchDTO roomsSearchDTO,int id)
        {
            Enum.TryParse(roomsSearchDTO.RoomType, true, out RoomType roomType);
            return await _dataContext.Rooms.Where(r => r.FloorId == id)
                                            .Where(r => (r.RoomNumber.Contains(roomsSearchDTO.RoomName)))
                                            .Where(r=>r.RoomType==roomType).ToListAsync();
        }
    }
}