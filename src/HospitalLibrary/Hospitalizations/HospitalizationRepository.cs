using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.MedicalRecords;
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

        public async Task<IEnumerable<Hospitalization>> GetAllForPatient(int id)
        {
            return await _dataContext.Hospitalizations
                .Where(h => h.MedicalRecord.PatientId == id)
                .OrderBy(h => h.StartTime)
                .ToListAsync();
        }
    }
}