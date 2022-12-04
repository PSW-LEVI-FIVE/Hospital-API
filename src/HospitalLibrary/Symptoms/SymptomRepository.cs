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
    }
}