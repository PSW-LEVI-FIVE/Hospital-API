using System.Threading.Tasks;

namespace HospitalLibrary.Examination.Interfaces
{
    public interface IExaminationReportService
    {
        public Task<ExaminationReport> Create(ExaminationReport report);
        public ExaminationReport GetByExamination(int examinationId);
        public ExaminationReport GetById(int id);

        public Task<ExaminationReport> Update(ExaminationReport report);
    }
}