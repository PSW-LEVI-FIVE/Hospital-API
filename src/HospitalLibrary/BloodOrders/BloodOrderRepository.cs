using HospitalLibrary.BloodOrders.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.BloodOrders
{
    public class BloodOrderRepository : BaseRepository<BloodOrder>, IBloodOrderRepository
    {
        public BloodOrderRepository(HospitalDbContext context) : base(context)
        {
        }
    }
}