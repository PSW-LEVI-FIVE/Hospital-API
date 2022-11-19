using HospitalLibrary.Hospitalizations.Dtos;

namespace HospitalLibrary.Hospitalizations.Interfaces
{
    public interface IHospitalizationValidator
    {
        void ValidateCreate(Hospitalization hospitalization);
        void ValidateEndHospitalization(Hospitalization hospitalization, EndHospitalizationDTO dto);
        void ValidateHospitalizationForPdfGeneration(Hospitalization hospitalization);
    }
}