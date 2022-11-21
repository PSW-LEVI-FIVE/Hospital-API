using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.BloodStorages.Interfaces
{
    public interface IBloodStorageService
    {
        bool SubtractQuantity(BloodStorage blood, double quantity);

        Task<BloodStorage> GetByType(BloodType type);

        Task<IEnumerable<BloodStorage>> GetAllBloodStorage();
    }
}