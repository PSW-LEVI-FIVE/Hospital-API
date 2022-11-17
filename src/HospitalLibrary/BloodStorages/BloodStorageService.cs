using System;
using System.Threading.Tasks;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.BloodStorages
{
    public class BloodStorageService : IBloodStorageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BloodStorageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool SubtractQuantity(BloodStorage blood, double quantity)
        {
            CheckAmount(blood.Quantity, quantity);
            blood.Quantity -= quantity;
            _unitOfWork.BloodStorageRepository.Update(blood);
            return true;
        }

        private void CheckAmount(double onStorage, double toTake)
        {
            if (onStorage < toTake)
                throw new BadRequestException("Not enough blood in storage");
        }

        public async Task<BloodStorage> GetByType(BloodType type)
        {
            BloodStorage blood = await _unitOfWork.BloodStorageRepository.GetByType(type);
            return blood;
        }
    }
}