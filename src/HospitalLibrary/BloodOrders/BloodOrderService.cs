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
    }
}