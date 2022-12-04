using HospitalLibrary.Examination;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Symptoms.Interfaces;

namespace HospitalLibrary.Symptoms
{
    public class SymptomService: ISymptomService
    {
        private IUnitOfWork _unitOfWork;

        public SymptomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Symptom Create(Symptom symptom)
        {
            _unitOfWork.SymptomRepository.Add(symptom);
            _unitOfWork.SymptomRepository.Save();
            return symptom;
        }
    }
}