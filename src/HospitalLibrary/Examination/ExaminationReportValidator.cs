using HospitalLibrary.Appointments;
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
            Appointment appointment = _unitOfWork.AppointmentRepository.GetOne(existing.ExaminationId);

            if (appointment.Type != AppointmentType.EXAMINATION)
                throw new BadRequestException("Appointment is not of type examination!");
            if (existing != null) 
                throw new BadRequestException("Report for given examination has already been generated!");
            
        }
    }
}