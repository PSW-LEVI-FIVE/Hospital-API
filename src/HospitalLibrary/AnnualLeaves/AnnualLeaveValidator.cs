using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.AnnualLeaves.Dtos;

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
                throw new Shared.Exceptions.BadRequestException("Date is not valid!");

            var doctorAnnualLeaves = _unitOfWork.AnnualLeaveRepository.GetAllByDoctorId(annualLeave.DoctorId);

            ThrowIfAnnualLeavesOverlaps(annualLeave, doctorAnnualLeaves);
            
            var isDoctorAvailable = IsDoctorAvailable(annualLeave.DoctorId,
                new TimeInterval(annualLeave.StartAt, annualLeave.EndAt));

            if (!annualLeave.IsUrgent && !isDoctorAvailable)
            {
                throw new Shared.Exceptions.BadRequestException("There are appointments in given period");
            }
            if (annualLeave.IsUrgent)
            {
               await _appointmentRescheduler.Reschedule(annualLeave.DoctorId, new TimeInterval(annualLeave.StartAt, annualLeave.EndAt));
            }
        }

        public void CancelValidation(AnnualLeave leave, int doctorId)
        {
            if(leave.State!=AnnualLeaveState.PENDING)
                throw new Shared.Exceptions.BadRequestException("Annual Leave isn't PENDING,can not cancel it!");
            if(leave.DoctorId!=doctorId)
                throw new Shared.Exceptions.BadRequestException("Doctor and Annual-Leave don't match!");
        }

        private bool IsDoctorAvailable(int doctorId, TimeInterval timeInterval)
        {
            var numberOfAppointments =
                _unitOfWork.AppointmentRepository.GetNumberOfDoctorAppointmentsForRange(doctorId, timeInterval);
            return numberOfAppointments == 0;
        }

        private void ThrowIfAnnualLeavesOverlaps(AnnualLeave annualLeaveToMake, IEnumerable<AnnualLeave> annualLeaves)
        {
            foreach (var al in annualLeaves)
            {
                if (al.StartAt < DateTime.Now || al.State == AnnualLeaveState.CANCELED || al.State == AnnualLeaveState.DELETED)
                {
                    continue;
                }
                TimeInterval timeInterval1 = new TimeInterval(al.StartAt, al.EndAt);
                TimeInterval timeInterval2 = new TimeInterval(annualLeaveToMake.StartAt, annualLeaveToMake.EndAt);
                if (timeInterval1.IsOverlaping(timeInterval2))
                {
                    throw new Shared.Exceptions.BadRequestException("There are already annual leaves in that period");
                }
            }
        }

        public void ReviewAnnualLeaveValidation(AnnualLeave leave, ReviewLeaveRequestDTO reviewLeaveRequestDTO)
        {
            if (leave == null)
                throw new NotFoundException("Annual leave with given id doesn't exist!");
            if (leave.State != AnnualLeaveState.PENDING)
                throw new Shared.Exceptions.BadRequestException("Annual leave isn't pending, can't review it!");
            if (reviewLeaveRequestDTO.State == AnnualLeaveState.CANCELED && reviewLeaveRequestDTO.Reason == null)
                throw new Shared.Exceptions.BadRequestException("Can't reject annual leave request without reason!");
        }
    }
}
