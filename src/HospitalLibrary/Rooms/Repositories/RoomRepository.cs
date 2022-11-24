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

        public async Task<IEnumerable<Room>> SearchByName(RoomSearchDTO roomSearchDTO)
        {
            return await _dataContext.Rooms.Where(r => r.RoomNumber.Contains(roomSearchDTO.RoomName)).ToListAsync();
        }

        public async Task<IEnumerable<Room>> SearchByType(RoomSearchDTO roomSearchDTO)
        {
            return await _dataContext.Rooms.Where(r => r.RoomType==(roomSearchDTO.RoomType)).ToListAsync();
        }

        public async Task<IEnumerable<Room>> SearchByTypeAndName(RoomSearchDTO roomsSearchDTO)
        {
            
            return await _dataContext.Rooms.Where(r => (r.RoomNumber.Contains(roomsSearchDTO.RoomName)))
            .Where(r=>r.RoomType==roomsSearchDTO.RoomType).ToListAsync();
        }
    }
}