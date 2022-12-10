using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Examination;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Symptoms.Interfaces
{
    public interface ISymptomRepository: IBaseRepository<Symptom>
    {
        IEnumerable<Symptom> PopulateRange(IEnumerable<Symptom> symptoms);
        IEnumerable<Symptom> Search(string name);
        
    }
}