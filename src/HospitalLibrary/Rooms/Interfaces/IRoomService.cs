using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAll();
        Room Update(Room room);
        Room GetOne(int key);
    }
}