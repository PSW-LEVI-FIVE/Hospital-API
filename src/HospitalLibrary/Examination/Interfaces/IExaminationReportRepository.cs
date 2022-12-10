using System.Collections.Generic;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Examination.Interfaces
{
    public interface IExaminationReportRepository: IBaseRepository<ExaminationReport>
    {
        public ExaminationReport GetByExamination(int examinationId);
    }
}