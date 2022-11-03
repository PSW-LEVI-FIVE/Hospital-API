using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Map.Interfaces
{
    public interface IMapRoomRepository: IBaseRepository<MapRoom>
    {
        Task<IEnumerable<MapRoom>> GetRoomsByFloor(int floorId);
    }
}