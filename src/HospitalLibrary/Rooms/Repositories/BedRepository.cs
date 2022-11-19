using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Rooms.Repositories
{
    public class BedRepository: BaseRepository<Bed>, IBedRepository
    {
        public BedRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<Bed>> GetAllFreeBedsForRoom(int roomId)
        {
            return await _dataContext.Beds
                .Where(bed => bed.AllHospitalizations.TrueForAll(h => h.State == HospitalizationState.FINISHED))
                .ToListAsync();
        }

        public bool IsBedFree(int id)
        {
            Hospitalization hsHospitalization = _dataContext.Hospitalizations
                .Where(h => h.BedId == id)
                .FirstOrDefault(h => h.State == HospitalizationState.ACTIVE);
            return hsHospitalization == null;
        }
    }
}