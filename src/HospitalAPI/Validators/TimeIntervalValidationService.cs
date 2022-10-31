using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalAPI.ErrorHandling.Exceptions;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Interfaces;


namespace HospitalAPI.Validators
{
    public class TimeIntervalValidationService: ITimeIntervalValidationService
    {
        private IUnitOfWork _unitOfWork;

        public TimeIntervalValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task ValidateAppointment(Appointment appointment)
        {
            if (IsAppointmentInPast(appointment))
            {
                throw new CustomBadRequestException("This time interval is in the past");
            }
            
            IEnumerable<TimeInterval> doctorTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllDoctorTakenIntervalsForDate(appointment.DoctorId,
                    appointment.StartAt.Date);
            
            IEnumerable<TimeInterval> roomTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(appointment.RoomId,
                    appointment.StartAt.Date);
            
            IEnumerable<TimeInterval> mixedIntervals = doctorTimeIntervals.Concat(roomTimeIntervals);

            TimeInterval requestedTimeInterval = new TimeInterval(appointment.StartAt, appointment.EndAt);
            if (CheckIfIntervalsAreOverlaping(mixedIntervals.ToList(), requestedTimeInterval))
            {
                throw new CustomBadRequestException("This time interval is already in use");
            }
        }

        private bool IsAppointmentInPast(Appointment appointment)
        {
            return appointment.StartAt.Date.CompareTo(DateTime.Now.AddDays(1).Date) < 0;
        }
        
        private bool CheckIfIntervalsAreOverlaping(List<TimeInterval> intervals, TimeInterval ti)
        {
            return intervals.Any(interval => interval.IsOverlaping(ti));
        }
        
        private List<TimeInterval> CompactIntervals(List<TimeInterval> intervals)
        {
            List<TimeInterval> compactedIntervals = new List<TimeInterval>();
            foreach (TimeInterval dateInterval in intervals)
            {
                int newIntervalCounter = compactedIntervals.Count == 0 ? 0 : compactedIntervals.Count - 1;
                AddFirstIfIntervalEmpty(compactedIntervals, dateInterval);
                JoinIntervalsIfTouching(compactedIntervals[newIntervalCounter], dateInterval);
                AddIfGapBetweenIntervals(compactedIntervals, compactedIntervals[newIntervalCounter], dateInterval);
            }

            return compactedIntervals;
        }

        private void JoinIntervalsIfTouching(TimeInterval interval, TimeInterval dateInterval)
        {
            if (interval.IntervalsTouching(dateInterval))
            {
                interval.End = dateInterval.End;
            }
        }

        private void AddFirstIfIntervalEmpty(List<TimeInterval> dateIntervals, TimeInterval interval)
        {
            if (dateIntervals.Count == 0)
            {
                dateIntervals.Add(new TimeInterval(interval));
            }
        }

        private void AddIfGapBetweenIntervals(List<TimeInterval> intervals, TimeInterval interval, TimeInterval dateInterval)
        {
            if (interval.IsThereGapInIntervals(dateInterval))
            {
                intervals.Add(new TimeInterval(dateInterval));
            }
        }
    }
}