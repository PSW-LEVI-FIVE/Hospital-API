using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Therapies
{
    public class TherapyRepository: BaseRepository<Therapy>, ITherapyRepository
    {
        public TherapyRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public  IEnumerable<Therapy> GetAllByHospitalization(int hospitalizationId)
        {
            return  _dataContext.Therapies
                .Where(t => t.HospitalizationId == hospitalizationId)
                .ToList();
        }
    }
}