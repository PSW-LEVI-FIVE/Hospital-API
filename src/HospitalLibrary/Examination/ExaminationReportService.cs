using System.Collections.Generic;
using System.Linq;
using ceTe.DynamicPDF.PageElements;
using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Symptoms;

namespace HospitalLibrary.Examination
{
    public class ExaminationReportService: IExaminationReportService
    {

        private IUnitOfWork _unitOfWork;

        public ExaminationReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public ExaminationReport Create(ExaminationReport report)
        {
            var symptoms = _unitOfWork.SymptomRepository.PopulateRange(report.Symptoms);
            var notExisting = FindNotExisting(report.Symptoms, symptoms.ToList());
            symptoms = symptoms.Concat(notExisting);
            report.Symptoms = symptoms.ToList();
            _unitOfWork.ExaminationReportRepository.Add(report);
            _unitOfWork.ExaminationReportRepository.Save();
            return report;
        }
        
        public ExaminationReport GetByExamination(int examinationId)
        {
            return _unitOfWork.ExaminationReportRepository.GetByExamination(examinationId);
        }

        public ExaminationReport GetById(int id)
        {
            return _unitOfWork.ExaminationReportRepository.GetOne(id);
        }


        private IEnumerable<Symptom> FindNotExisting(List<Symptom> old, List<Symptom> existing)
        {
            return old.Where(s => existing.All(e => e.Id != s.Id)).ToList();
        }
    }
}