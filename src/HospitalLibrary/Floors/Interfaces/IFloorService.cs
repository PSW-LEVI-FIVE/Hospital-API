using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Floors.Interfaces
{
    public interface IFloorService
    {
        Task<IEnumerable<Floor>> GetAll();
        Floor Update(Floor floor);
    }
}