using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository.Interfaces;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class RoomRepository: BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<Room>> FindAllByFloor(int floor)
        {
            return await _dataContext.Rooms.Where(r => r.Floor == floor).ToListAsync();
        }
    }
}