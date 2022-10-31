﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Shared.Validators
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
            ThrowIfEndBeforeStart(appointment.StartAt, appointment.EndAt);
            ThrowIfInPast(appointment.StartAt);
            
            IEnumerable<TimeInterval> doctorTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllDoctorTakenIntervalsForDate(appointment.DoctorId,
                    appointment.StartAt.Date);
            
            IEnumerable<TimeInterval> roomTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(appointment.RoomId,
                    appointment.StartAt.Date);
            
            IEnumerable<TimeInterval> mixedIntervals = doctorTimeIntervals.Concat(roomTimeIntervals);

            TimeInterval requestedTimeInterval = new TimeInterval(appointment.StartAt, appointment.EndAt);
            
            ThrowIfIntervalsAreOverlaping(mixedIntervals.ToList(), requestedTimeInterval);
        }

        public async Task ValidateRescheduling(Appointment appointment, DateTime start, DateTime end)
        {
            ThrowIfEndBeforeStart(start, end);
            ThrowIfInPast(start);
            
            IEnumerable<TimeInterval> doctorTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllDoctorTakenIntervalsForDate(appointment.DoctorId,
                    start.Date);
            
            IEnumerable<TimeInterval> roomTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDate(appointment.RoomId,
                    end.Date);
            
            IEnumerable<TimeInterval> mixedIntervals = doctorTimeIntervals.Concat(roomTimeIntervals);

            TimeInterval requestedTimeInterval = new TimeInterval(start, end);
            TimeInterval except = new TimeInterval(appointment.StartAt, appointment.EndAt);
            ThrowIfIntervalsAreOverlaping(mixedIntervals.ToList(), requestedTimeInterval, except);
        }

        private void ThrowIfEndBeforeStart(DateTime start, DateTime end)
        {
            if (start.CompareTo(end) >= 0)
            {
                throw new BadRequestException("Start time cannot be before end time");
            }
        }
        
        private void ThrowIfInPast(DateTime start)
        {
            if (start.Date.CompareTo(DateTime.Now.AddDays(1).Date) < 0)
            {
                throw new BadRequestException("This time interval is in the past");
            }
        }
        
        private void ThrowIfIntervalsAreOverlaping(List<TimeInterval> intervals, TimeInterval ti)
        {
            if (intervals.Any(interval => interval.IsOverlaping(ti)))
            {
                throw new BadRequestException("This time interval is already in use");
            }
        }
        
        private void ThrowIfIntervalsAreOverlaping(List<TimeInterval> intervals, TimeInterval ti, TimeInterval except)
        {
            if (intervals.Any(interval => interval.IsOverlaping(ti)) && !intervals.Any(interval => interval.IsOverlaping(except)))
            {
                throw new BadRequestException("This time interval is already in use");
            }
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