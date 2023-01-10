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

        public IEnumerable<ExaminationReport> SearchByPhrase(string phrase)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<ExaminationReport> SearchByWords(string word)
        {
            return _dataContext.ExaminationReports
                .Include(a => a.Symptoms)
                .Include(a=> a.Prescriptions)
                .Include(a => a.Examination)
                .Include(a => a.Doctor)
                .Where(a => a.Content!=null)
                .Where(a => a.Content.ToLower().Contains(word) ||
                    a.Symptoms.Any(sym => word.Contains(sym.Name.ToLower())))
                    // a.Prescriptions.Any(pre => word.Contains(pre.Medicine.Name.NameString.ToLower())))
                .OrderByDescending(a=>a.Examination.EndAt)
                .ToList();
        }
    }
}