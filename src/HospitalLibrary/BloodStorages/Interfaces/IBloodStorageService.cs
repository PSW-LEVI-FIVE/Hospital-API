using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.BloodStorages.Dtos;

namespace HospitalLibrary.BloodStorages.Interfaces
{
    public interface IBloodStorageService
    {
        bool SubtractQuantity(BloodStorage blood, double quantity);

        Task<BloodStorage> GetByType(BloodType type);

        Task<List<BloodStorageDto>> GetAllBloodStorage();
        List<BloodType> GetAllCompatibileBloodStorage(int hospitalizationId);
    }
}