using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Allergens.Interfaces
{
    public interface IAllergenRepository: IBaseRepository<Allergen>
    {
        Task<Allergen> GetOneByName(string name);
        Task<IEnumerable<Allergen>> GetAllergensWithNumberOfPatients();

        List<int> GetAllergenIdsByPatient(int patientId);
    }
}