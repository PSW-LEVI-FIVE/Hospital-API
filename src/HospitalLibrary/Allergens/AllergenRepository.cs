using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Allergens.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Allergens
{
    public class AllergenRepository: BaseRepository<Allergen>, IAllergenRepository
    {
        public AllergenRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<Allergen>> GetAllergensWithNumberOfPatients()
        {
            return await _dataContext.Allergens
                .OrderByDescending(allergen => allergen.Patients.Count)
                .Include(allergen => allergen.Patients)
                .ToListAsync();
        }

        public Task<Allergen> GetOneByName(string name)
        {
            return _dataContext.Allergens.Where(a => a.Name.Equals(name)).FirstOrDefaultAsync();
        }
    }
}