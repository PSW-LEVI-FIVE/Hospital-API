using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Rooms.Interfaces
{
    public interface IRoomRepository: IBaseRepository<Room>
    {
        Task<IEnumerable<Room>> FindAllByFloor(int floor);
    }
}