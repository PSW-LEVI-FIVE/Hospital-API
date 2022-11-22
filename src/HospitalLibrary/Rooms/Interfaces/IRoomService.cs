using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAll();
        Room Update(Room room);
        Room GetOne(int key);
        IEnumerable<Bed> GetBedsForRoom(int id);

        Room Create(Room room);

    }
}