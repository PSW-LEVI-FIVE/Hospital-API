namespace HospitalLibrary.Hospitalizations.Interfaces
{
    public interface IHospitalizationValidator
    {
        void ValidateCreate(Hospitalization hospitalization);
    }
}