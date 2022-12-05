using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Examination;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Symptoms.Interfaces
{
    public interface ISymptomService
    {
        public Symptom Create(Symptom symptom);
        public Task<IEnumerable<Symptom>> GetAll();
    }
}