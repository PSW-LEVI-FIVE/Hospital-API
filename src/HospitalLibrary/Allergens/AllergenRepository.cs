using HospitalLibrary.Allergens.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Allergens
{
    public class AllergenRepository: BaseRepository<Allergen>, IAllergenRepository
    {
        public AllergenRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}