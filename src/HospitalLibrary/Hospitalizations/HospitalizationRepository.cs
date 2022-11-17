using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

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
                .Include(h => h.MedicalRecord)
                .Include(h => h.Therapies)
                .FirstOrDefault();
        }

    }
}