using HospitalLibrary.Hospitalizations.Dtos;
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
            _validator.ValidateCreate(hospObj);
            _unitOfWork.HospitalizationRepository.Add(hospObj);
            _unitOfWork.HospitalizationRepository.Save();
            return hospObj;
        }

        public Hospitalization EndHospitalization(int id, EndHospitalizationDTO dto)
        {
            Hospitalization hospitalization = _unitOfWork.HospitalizationRepository.GetOne(id);
            _validator.ValidateEndHospitalization(hospitalization, dto);

            hospitalization.State = HospitalizationState.FINISHED;
            hospitalization.EndTime = dto.EndTime;
            
            _unitOfWork.HospitalizationRepository.Update(hospitalization);
            _unitOfWork.HospitalizationRepository.Save();
            return hospitalization;
        }
    }
}