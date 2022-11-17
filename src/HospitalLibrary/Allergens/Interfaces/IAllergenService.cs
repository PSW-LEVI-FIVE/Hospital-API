using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Allergens
{
    public interface IAllergenService
    {
        Task<IEnumerable<Allergen>> GetAll();
        Allergen Create(Allergen allergen);
    }
}