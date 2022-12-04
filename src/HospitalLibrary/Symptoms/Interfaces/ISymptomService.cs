using HospitalLibrary.Examination;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Symptoms.Interfaces
{
    public interface ISymptomService
    {
        public Symptom Create(Symptom symptom);
    }
}