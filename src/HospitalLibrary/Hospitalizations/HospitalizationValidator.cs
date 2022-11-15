using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Hospitalizations
{
    public class HospitalizationValidator: IHospitalizationValidator
    {
        private IUnitOfWork _unitOfWork;

        public HospitalizationValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public void ValidateCreate(Hospitalization hospitalization)
        {
            throw new System.NotImplementedException();
        }
    }
}