using System.Threading.Tasks;
using HospitalLibrary.BloodOrders.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.BloodOrders
{
    
    public class BloodOrderService : IBloodOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public BloodOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BloodOrder> Create(BloodOrder bloodOrder)
        {
            _unitOfWork.BloodOrderRepository.Add(bloodOrder);
            _unitOfWork.BloodOrderRepository.Save();
            return bloodOrder;
        }
    }
}