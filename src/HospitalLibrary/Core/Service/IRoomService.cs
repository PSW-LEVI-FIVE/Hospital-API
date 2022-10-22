using HospitalLibrary.Core.Model;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAll();
        Room GetById(int id);
        void Create(Room room);
        void Update(Room room);
        void Delete(Room room);
    }
}
