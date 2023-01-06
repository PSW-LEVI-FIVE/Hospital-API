using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

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

        public List<ExaminationReport> FindAllBySpecialty(int specialtyId)
        {
            return _dataContext.ExaminationReports
                .Where(e => e.Doctor.SpecialityId == specialtyId)
                .ToList();
        }
        public ExaminationReport FindExam() =>
            _dataContext.ExaminationReports
                .Include(a => a.Examination)
                .FirstOrDefault(e => e.Examination.Id == e.ExaminationId);
    }
}