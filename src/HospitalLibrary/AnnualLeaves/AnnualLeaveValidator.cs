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

        public AnnualLeaveValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Validate(AnnualLeave annualLeave)
        {
            if(!annualLeave.IsValid())
                throw new BadRequestException("Date is not valid!");
            var isDoctorAvailable = IsDoctorAvailable(annualLeave.DoctorId,
                new TimeInterval(annualLeave.StartAt, annualLeave.EndAt));
            if (!isDoctorAvailable)
            {
                throw new BadRequestException("There are appointments in given period");
            }
        }

        public void Cancel_Validation(AnnualLeave leave, int doctorId)
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