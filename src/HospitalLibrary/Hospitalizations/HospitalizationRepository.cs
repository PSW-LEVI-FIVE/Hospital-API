using System.Linq;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;


namespace HospitalLibrary.Hospitalizations
{
    public class HospitalizationRepository: BaseRepository<Hospitalization>, IHospitalizationRepository
    {
        public HospitalizationRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public Hospitalization GetOnePopulated(int id)
        {
            return _dataContext.Hospitalizations
                .Include(h => h.Bed)
                .Include(h => h.Therapies)                
                .Include(h => h.MedicalRecord)
                .ThenInclude(m => m.Patient)
                .FirstOrDefault();
        }

    }
}