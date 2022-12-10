using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Medicines.Interfaces
{
    public interface IMedicineService
    {
        bool SubtractQuantity(int medicine, double quantity);
        Task<IEnumerable<Medicine>> GetAllMedicine();

        IEnumerable<Medicine> GetAllCompatibileMedicine(int hospitalizationId);

        IEnumerable<Medicine> Search(string name);
    }
}