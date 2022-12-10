namespace HospitalLibrary.Examination.Interfaces
{
    public interface IExaminationReportService
    {
        public ExaminationReport Create(ExaminationReport report);
        public ExaminationReport GetByExamination(int examinationId);
        public ExaminationReport GetById(int id);
    }
}