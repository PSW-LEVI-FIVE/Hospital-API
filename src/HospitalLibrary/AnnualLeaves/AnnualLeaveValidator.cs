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

        private bool IsDoctorAvailable(int doctorId, TimeInterval timeInterval)
        {
            var numberOfAppointments =
                _unitOfWork.AppointmentRepository.GetNumberOfDoctorAppointmentsForRange(doctorId, timeInterval);
            return numberOfAppointments == 0;
        }
    }
}