using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.Appointments;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Shared.Validators
{
    public class TimeIntervalValidationService: ITimeIntervalValidationService
    {
        private IUnitOfWork _unitOfWork;

        public TimeIntervalValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task ValidateRenovation(Renovation.Model.Renovation renovation)
        {

            ThrowIfEndBeforeStart(renovation.StartAt, renovation.EndAt);
            ThrowIfInPast(renovation.StartAt);

            IEnumerable<TimeInterval> startingRoomTimeIntervals =
                await _unitOfWork.EquipmentReallocationRepository.GetAllRoomTakenInrevalsForDate( renovation.MainRoomId,
                    renovation.StartAt.Date);
            IEnumerable<TimeInterval> destinationRoomTimeIntervals =
                await _unitOfWork.EquipmentReallocationRepository.GetAllRoomTakenInrevalsForDate(renovation.SecondaryRoomId,
                    renovation.StartAt.Date);

            IEnumerable<TimeInterval> mixedIntervals = startingRoomTimeIntervals.Concat(destinationRoomTimeIntervals);

            TimeInterval requestedTimeInterval = new TimeInterval(renovation.StartAt, renovation.EndAt);

            ThrowIfIntervalsAreOverlaping(mixedIntervals.ToList(), requestedTimeInterval);
        }
        public async Task ValidateReallocation(EquipmentReallocation reallocation)
        {
            ThrowIfEndBeforeStart(reallocation.StartAt, reallocation.EndAt);
            ThrowIfInPast(reallocation.StartAt);

            IEnumerable<TimeInterval> startingRoomTimeIntervals =
                await _unitOfWork.EquipmentReallocationRepository.GetAllRoomTakenInrevalsForDate(reallocation.StartingRoomId,
                    reallocation.StartAt.Date);
            IEnumerable<TimeInterval> destinationRoomTimeIntervals =
                await _unitOfWork.EquipmentReallocationRepository.GetAllRoomTakenInrevalsForDate(reallocation.DestinationRoomId,
                    reallocation.StartAt.Date);

            IEnumerable<TimeInterval> mixedIntervals = startingRoomTimeIntervals.Concat(destinationRoomTimeIntervals);

            TimeInterval requestedTimeInterval = new TimeInterval(reallocation.StartAt, reallocation.EndAt);

            ThrowIfIntervalsAreOverlaping(mixedIntervals.ToList(), requestedTimeInterval);
        }


        public async Task<bool> IsIntervalOverlapingWithDoctorAppointments(int doctorId,TimeInterval possibleTimeInterval)
        {
            IEnumerable<TimeInterval> doctorsAppointmentsTimeIntervals = await _unitOfWork.AppointmentRepository
                .GetAllDoctorTakenIntervalsForDate(doctorId,possibleTimeInterval.Start);
            return doctorsAppointmentsTimeIntervals.Any(possibleTimeInterval.IsOverlaping);
        }

        public async Task<bool> IsTimeIntervalOverlapingWithRoomsAppointments(int roomId,TimeInterval possibleTimeInterval)
        {
            IEnumerable<TimeInterval> roomAppointmentsTimeIntervals =await _unitOfWork.AppointmentRepository
                .GetAllRoomTakenIntervalsForDate(roomId, possibleTimeInterval.Start);
            return roomAppointmentsTimeIntervals.Any(possibleTimeInterval.IsOverlaping);
        }

        public async Task ValidateAppointment(Appointment appointment)
        {
            ThrowIfEndBeforeStart(appointment.StartAt, appointment.EndAt);
            ThrowIfInPast(appointment.StartAt);
            ThrowIfInAnnualLeavePeriod(appointment.DoctorId, new TimeInterval(appointment.StartAt, appointment.EndAt));
            ThrowIfNotInWorkingHours(appointment);

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
        
        private void ThrowIfNotInWorkingHours(Appointment appointment)
        {
            ThrowIfNotInWorkingHours(appointment.StartAt, appointment.EndAt, appointment.DoctorId);
        }
        
        private void ThrowIfNotInWorkingHours(DateTime appointmentStartAt, DateTime appointmentEndAt, int doctorId)
        {
            WorkingHours workingHours = _unitOfWork.WorkingHoursRepository.GetOne((int) appointmentStartAt.DayOfWeek, doctorId);
            TimeInterval requestedTimeInterval = new TimeInterval(appointmentStartAt, appointmentEndAt);
            TimeInterval workingHoursTimeInterval = new TimeInterval(appointmentStartAt, workingHours.Start, workingHours.End);
            if (!TimeInterval.IsIntervalInside(workingHoursTimeInterval, requestedTimeInterval))
            {
                throw new BadRequestException("Requested time does not follow the working hours rule");
            }
        }

        public async Task ValidateRescheduling(Appointment appointment, DateTime start, DateTime end)
        {
            ThrowIfEndBeforeStart(start, end);
            ThrowIfInPast(start);
            ThrowIfInAnnualLeavePeriod(appointment.DoctorId, new TimeInterval(start, end));
            ThrowIfNotInWorkingHours(start, end, appointment.DoctorId);
            
            IEnumerable<TimeInterval> doctorTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllDoctorTakenIntervalsForDateExcept(appointment.DoctorId,
                    start.Date, appointment.Id);
            
            IEnumerable<TimeInterval> roomTimeIntervals =
                await _unitOfWork.AppointmentRepository.GetAllRoomTakenIntervalsForDateExcept(appointment.RoomId,
                    end.Date, appointment.Id);
            
            IEnumerable<TimeInterval> mixedIntervals = doctorTimeIntervals.Concat(roomTimeIntervals);

            TimeInterval requestedTimeInterval = new TimeInterval(start, end);
            ThrowIfIntervalsAreOverlaping(mixedIntervals.ToList(), requestedTimeInterval);
        }

        public void ThrowIfEndBeforeStart(DateTime start, DateTime end)
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

        private void ThrowIfInAnnualLeavePeriod(int doctorId, TimeInterval timeInterval)
        {
            IEnumerable<AnnualLeave> annualLeaves =
                _unitOfWork.AnnualLeaveRepository.GetDoctorsAnnualLeavesInRange(doctorId, timeInterval);
            if (annualLeaves.Any())
            {
                throw new BadRequestException("There is an annual leave in that period");
            }
        }

       
    }
}