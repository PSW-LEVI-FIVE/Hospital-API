using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Examination
{
    public class ExaminationReportRepository: BaseRepository<ExaminationReport>, IExaminationReportRepository
    {
        public ExaminationReportRepository(HospitalDbContext dataContext) : base(dataContext)
        {
        }

        public ExaminationReport GetByExamination(int examinationId)
        {
            return _dataContext.ExaminationReports.FirstOrDefault(e => e.ExaminationId == examinationId);
        }

    }
}