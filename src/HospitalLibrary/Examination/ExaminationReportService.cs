using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Shared.Interfaces;

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
    }
}