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
    }
}