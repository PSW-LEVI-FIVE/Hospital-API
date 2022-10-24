using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository.Interfaces
{
    public interface IRoomRepository: IBaseRepository<Room>
    {
        Task<IEnumerable<Room>> FindAllByFloor(int floor);
    }
}