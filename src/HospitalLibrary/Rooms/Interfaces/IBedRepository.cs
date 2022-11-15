using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IBedRepository: IBaseRepository<Bed>
    {
        Task<IEnumerable<Bed>> GetAllFreeBedsForRoom(int roomId);
    }
}