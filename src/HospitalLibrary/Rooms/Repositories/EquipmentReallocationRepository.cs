using HospitalLibrary.Appointments;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Rooms.Repositories
{
    public class EquipmentReallocationRepository : BaseRepository<EquipmentReallocation>, IEquipmentReallocationRepository
    {
        public EquipmentReallocationRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<TimeInterval>> GetAllRoomTakenInrevalsForDate(int roomId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<EquipmentReallocation> GetById(int appointmentId)
        {
            throw new NotImplementedException();
        }
    }
}
