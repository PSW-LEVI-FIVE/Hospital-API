using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalLibrary.Examination;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using HospitalLibrary.Symptoms.Interfaces;

namespace HospitalLibrary.Symptoms
{
    public class SymptomRepository: BaseRepository<Symptom>, ISymptomRepository
    {
        public SymptomRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<Symptom> PopulateRange(IEnumerable<Symptom> symptoms)
        {
            var ids = symptoms.Select(s => s.Id);
            return _dataContext.Symptoms
                .Where(s => ids.Any(id => id == s.Id))
                .ToList();
        }
    }
}