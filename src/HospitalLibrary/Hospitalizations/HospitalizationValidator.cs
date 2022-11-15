using HospitalLibrary.Hospitalizations.Dtos;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Shared.Exceptions;
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
            if (!_unitOfWork.MedicalRecordRepository.Exists(hospitalization.MedicalRecordId))
                throw new BadRequestException("Medical record doesn't exist!");
            if (!_unitOfWork.BedRepository.IsBedFree(hospitalization.BedId))
                throw new BadRequestException("Bed is currently taken!");
        }

        public void ValidateEndHospitalization(EndHospitalizationDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}