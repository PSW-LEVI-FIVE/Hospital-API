using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IBedService
    {
        Task<IEnumerable<Bed>> GetAll();
        IEnumerable<Bed> GetAllFreeForRoom(int roomId);
    }
}