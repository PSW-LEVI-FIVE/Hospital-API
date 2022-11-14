using HospitalLibrary.Medicines.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Medicines
{
    public class MedicineRepository: BaseRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }
    }
}