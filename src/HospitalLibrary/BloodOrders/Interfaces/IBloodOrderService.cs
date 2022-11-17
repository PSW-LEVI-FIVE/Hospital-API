using System.Threading.Tasks;

namespace HospitalLibrary.BloodOrders.Interfaces
{
    public interface IBloodOrderService
    {
        Task<BloodOrder> Create(BloodOrder bloodOrder);
    }
}