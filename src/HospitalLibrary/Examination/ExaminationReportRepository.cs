using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Examination.Dtos;
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
        
        public IEnumerable<SearchResultDTO> Search(List<string> words)
        {
            return _dataContext.ExaminationReports
                .Include(a => a.Symptoms)
                .Include(a => a.Examination)
                .Include(a => a.Doctor)
                .Include(a=> a.Prescriptions)
                .ThenInclude(pre => pre.Medicine)
                .Where(a => a.Content!=null)
                .AsEnumerable()
                .Where(a => 
                    words.Any(word => 
                        a.Content.ToLower().Contains(word) ||
                        a.Symptoms.Any(sym => word.Contains(sym.Name.ToLower())) ||
                        a.Symptoms.Any(sym => sym.Name.ToLower().Contains(word.ToLower())) ||
                        a.Prescriptions.Any(pre => word.Contains(pre.Medicine.Name.NameString.ToLower())) ||
                        a.Prescriptions.Any(pre => pre.Medicine.Name.NameString.ToLower().Contains(word.ToLower()))) 
                    )
                .OrderByDescending(a=>a.Examination.EndAt)
                .Select(exam => new SearchResultDTO(exam))
                .ToList();
        }
    }
}