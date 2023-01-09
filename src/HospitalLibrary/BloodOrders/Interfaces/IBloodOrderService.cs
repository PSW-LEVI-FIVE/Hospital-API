using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.BloodOrders.Dtos;

namespace HospitalLibrary.BloodOrders.Interfaces
{
    public interface IBloodOrderService
    {
        Task<BloodOrder> Create(BloodOrder bloodOrder);
        Task<List<ShowBloodOrderDto>> GetAllBloodOrders();
    }
}