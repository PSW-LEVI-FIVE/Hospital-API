using HospitalLibrary.Hospitalizations.Dtos;

namespace HospitalLibrary.Hospitalizations.Interfaces
{
    public interface IHospitalizationValidator
    {
        void ValidateCreate(Hospitalization hospitalization);
        void ValidateEndHospitalization(EndHospitalizationDTO dto);
    }
}