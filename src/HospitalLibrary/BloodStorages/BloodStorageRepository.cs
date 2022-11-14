using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.BloodStorages
{
    public class BloodStorageRepository: BaseRepository<BloodStorage>, IBloodStorageRepository
    {
        public BloodStorageRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}