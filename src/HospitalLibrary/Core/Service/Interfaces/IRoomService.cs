using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAll();
    }
}