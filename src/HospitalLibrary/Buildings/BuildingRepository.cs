using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Buildings
{
    public class BuildingRepository: BaseRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}