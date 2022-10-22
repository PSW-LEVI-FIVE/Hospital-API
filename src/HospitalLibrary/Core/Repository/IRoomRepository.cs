using HospitalLibrary.Core.Model;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAll();
        Room GetById(int id);
        void Create(Room room);
        void Update(Room room);
        void Delete(Room room);
    }
}
