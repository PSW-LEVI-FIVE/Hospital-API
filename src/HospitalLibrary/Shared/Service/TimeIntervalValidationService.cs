using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Shared.Service
{
    public class TimeIntervalValidationService: ITimeIntervalValidationService
    {
        private IUnitOfWork _unitOfWork;

        public TimeIntervalValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async void ValidateAppointment(Appointment appointment)
        {
            IEnumerable<TimeInterval> doctorTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllDoctorTakenIntervalsForDate(appointment.DoctorId,
                    appointment.StartAt.Date);
            
            IEnumerable<TimeInterval> roomTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(appointment.RoomId,
                    appointment.StartAt.Date);
            
            IEnumerable<TimeInterval> mixedIntervals = doctorTimeIntervals.Concat(roomTimeIntervals);
            IEnumerable<TimeInterval> orderedIntervals = mixedIntervals.OrderBy(i => i.Start);
            List<TimeInterval> compactedIntervals = CompactIntervals(orderedIntervals.ToList());
            TimeInterval requestedTimeInterval = new TimeInterval(appointment.StartAt, appointment.EndAt);
            if (CheckIfIntervalsAreOverlaping(compactedIntervals, requestedTimeInterval))
            {
                throw new Exception("Intervals are overlapping");
            }
        }
        
        private bool CheckIfIntervalsAreOverlaping(List<TimeInterval> intervals, TimeInterval ti)
        {
            return intervals.All(interval => !interval.IsOverlaping(ti));
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