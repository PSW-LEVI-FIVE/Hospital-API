using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Hospitalizations
{
    public class HospitalizationService: IHospitalizationService
    {
        private IUnitOfWork _unitOfWork;
        private IHospitalizationValidator _validator;

        public HospitalizationService(IUnitOfWork unitOfWork, IHospitalizationValidator validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public Hospitalization Create(Hospitalization hospObj)
        {
            throw new System.NotImplementedException();
        }
    }
}