using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Allergens;
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
        
        public IEnumerable<Medicine> GetCompatibleForPatient(List<int> allergenIds)
        {
            return _dataContext.Medicines
                .Where(m => !m.Allergens.Any(a =>
                        allergenIds.Contains(a.Id)))
                .ToList();
        }

        public IEnumerable<Medicine> Search(string name)
        {
            return _dataContext.Medicines
                .Where(m => m.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }
    }
}