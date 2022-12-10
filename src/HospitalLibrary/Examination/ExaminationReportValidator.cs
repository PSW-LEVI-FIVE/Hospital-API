using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Exceptions;


namespace HospitalLibrary.Examination
{
    public class ExaminationReportValidator: IExaminationReportValidator
    {
        private IUnitOfWork _unitOfWork;

        public ExaminationReportValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public void ValidateCreate(ExaminationReport report)
        {
            ExaminationReport existing = _unitOfWork.ExaminationReportRepository.GetByExamination(report.ExaminationId);
            if (existing != null) 
                throw new BadRequestException("Report for given examination has already been generated!");
        }
    }
}