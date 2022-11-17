using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.BloodStorages
{
    public class BloodStorageRepository : BaseRepository<BloodStorage>, IBloodStorageRepository
    {
        public BloodStorageRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<BloodStorage> GetByType(BloodType type)
        {
            return await _dataContext.BloodStorage
                .Where(a => a.BloodType == type)
                .SingleAsync();
        }
    }
}