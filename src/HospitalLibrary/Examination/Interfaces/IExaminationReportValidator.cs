namespace HospitalLibrary.Examination.Interfaces
{
    public interface IExaminationReportValidator
    {
        void ValidateCreate(ExaminationReport report);
    }
}