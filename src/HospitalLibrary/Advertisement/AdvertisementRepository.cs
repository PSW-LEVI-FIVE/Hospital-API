using HospitalLibrary.Advertisement.Interfaces;
using HospitalLibrary.Allergens;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Advertisement
{
    public class AdvertisementRepository : BaseRepository<Advertisement> , IAdvertisementRepository
    {
        protected AdvertisementRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}