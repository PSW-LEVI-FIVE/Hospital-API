using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HospitalDbContext _context;

        public RoomRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAll()
        {
            return _context.Rooms.ToList();
        }

        public Room GetById(int id)
        {
            return _context.Rooms.Find(id);
        }

        public void Create(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void Update(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Room room)
        {
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }
    }
}
