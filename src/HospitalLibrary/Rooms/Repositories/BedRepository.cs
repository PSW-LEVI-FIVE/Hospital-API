
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Rooms.Repositories
{
    public class BedRepository: BaseRepository<Bed>, IBedRepository
    {
        public BedRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<Bed> GetAllFreeBedsForRoom(int roomId)
        {
            return _dataContext.Beds
                .Where(bed => bed.AllHospitalizations.All(h => h.State != HospitalizationState.ACTIVE))
                .ToList();
        }

        public bool IsBedFree(int id)
        {
            Hospitalization hsHospitalization = _dataContext.Hospitalizations
                .Where(h => h.BedId == id)
                .FirstOrDefault(h => h.State == HospitalizationState.ACTIVE);
            return hsHospitalization == null;
        }

        public IEnumerable<Bed> GetAllByRoom(int roomId)
        {
            return _dataContext.Beds
                .Where(b => b.RoomId == roomId)
                .ToList();
        }
    }
}