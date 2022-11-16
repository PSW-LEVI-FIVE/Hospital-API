using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.AnnualLeaves
{
    public class AnnualLeaveValidator : IAnnualLeaveValidator
    {
        private IUnitOfWork _unitOfWork;

        private IAppointmentRescheduler _appointmentRescheduler;

        public AnnualLeaveValidator(IUnitOfWork unitOfWork, IAppointmentRescheduler appointmentRescheduler)
        {
            _unitOfWork = unitOfWork;
            _appointmentRescheduler = appointmentRescheduler;
        }

        public async Task Validate(AnnualLeave annualLeave)
        {
            if(!annualLeave.IsValid())
                throw new BadRequestException("Date is not valid!");
            var isDoctorAvailable = IsDoctorAvailable(annualLeave.DoctorId,
                new TimeInterval(annualLeave.StartAt, annualLeave.EndAt));
            if (!annualLeave.IsUrgent && !isDoctorAvailable)
            {
                throw new BadRequestException("There are appointments in given period");
            }
            if (annualLeave.IsUrgent)
            {
               await _appointmentRescheduler.Reschedule(annualLeave.DoctorId, new TimeInterval(annualLeave.StartAt, annualLeave.EndAt));
            }
        }

        public void CancelValidation(AnnualLeave leave, int doctorId)
        {
            if(leave.State!=AnnualLeaveState.PENDING)
                throw new BadRequestException("Annual Leave isn't PENDING,can not cancel it!");
            if(leave.DoctorId!=doctorId)
                throw new BadRequestException("Doctor and Annual-Leave don't match!");
        }

        private bool IsDoctorAvailable(int doctorId, TimeInterval timeInterval)
        {
            var numberOfAppointments =
                _unitOfWork.AppointmentRepository.GetNumberOfDoctorAppointmentsForRange(doctorId, timeInterval);
            return numberOfAppointments == 0;
        }
    }
}
