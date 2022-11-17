using HospitalLibrary.Medicines.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Medicines
{
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool SubtractQuantity(int id, double quantity)
        {
            Medicine medicine = _unitOfWork.MedicineRepository.GetOne(id);
            CheckAmount(medicine.Quantity, quantity);
            medicine.Quantity -= quantity;
            _unitOfWork.MedicineRepository.Update(medicine);
            return true;
        }
        
        private void CheckAmount(double onStorage, double toTake)
        {
            if (onStorage < toTake)
                throw new BadRequestException("Not enough blood in storage");
        }
    }
}