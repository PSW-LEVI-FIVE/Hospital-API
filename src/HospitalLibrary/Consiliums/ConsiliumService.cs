using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Consiliums.Dtos;
using HospitalLibrary.Consiliums.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using LinqKit;

namespace HospitalLibrary.Consiliums
{
    public class ConsiliumService : IConsiliumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeIntervalValidationService _intervalValidation;

        public ConsiliumService(IUnitOfWork unitOfWork, ITimeIntervalValidationService intervalValidation)
        {
            _unitOfWork = unitOfWork;
            _intervalValidation = intervalValidation;
        }

        public async Task<Consilium> Create(Appointment appointment, List<int> doctors)
        {
            Consilium consilium = appointment.Consilium;
            _unitOfWork.ConsiliumRepository.Add(consilium);
            //TREBA DODATI PROVERU ZA SLOBODNU SOBU I DODELITI SVAKOM DOKTOR-APPOINTMENTU   
            foreach (int doctorId in doctors)
            {
                Appointment doctorAppointment = new Appointment(appointment);
                doctorAppointment.DoctorId = doctorId;
                _unitOfWork.AppointmentRepository.Add(doctorAppointment);
            }

            _unitOfWork.ConsiliumRepository.Save();
            _unitOfWork.AppointmentRepository.Save();
            return consilium;
        }

        public GetBestConsiliumsDTO SuggestConsilium(TimeInterval timeInterval, List<int> doctors, int schedulerDoctor,
            int consiliumDuration)
        {
            _intervalValidation.ThrowIfEndBeforeStart(timeInterval.Start, timeInterval.End);

            List<DateTime> dates = timeInterval.GetDatesForDateRange();
            Overlapper overallBest = GetBestDateTimeForConsilium(dates, doctors, consiliumDuration, schedulerDoctor);

            if (overallBest.OverlappedNumber == 0)
                throw new NotFoundException("Consilium for given parameters is not possible!");

            return new GetBestConsiliumsDTO(overallBest.BestConsiliumTime.Start, overallBest.BestConsiliumTime.End,
                overallBest.OverlappedDoctorsList, schedulerDoctor, consiliumDuration);
        }

        private Overlapper GetBestDateTimeForConsilium(List<DateTime> dates, List<int> doctors, int consiliumDuration,
            int schedulerDoctor)
        {
            Overlapper overallBest = new Overlapper(0, new List<int>(), new TimeInterval());
            foreach (DateTime date in dates)
            {
                List<TimeIntervalDoctor> freeTimes = GetFreeTimesForAllDoctors(doctors, date, consiliumDuration);
                List<List<TimeIntervalDoctor>> allPossibleSets = GenerateFreeTimeSets(freeTimes, schedulerDoctor);
                Overlapper dateBest = GetDateOverlapper(allPossibleSets, schedulerDoctor, consiliumDuration);

                if (dateBest.OverlappedNumber > overallBest.OverlappedNumber)
                    overallBest = dateBest;
            }

            return overallBest;
        }

        private List<TimeIntervalDoctor> GetFreeTimesForAllDoctors(List<int> doctors, DateTime date,
            int consiliumDuration)
        {
            List<TimeIntervalDoctor> freeTimes = new List<TimeIntervalDoctor>();
            IEnumerable<Doctor> datedDoctors = _unitOfWork.DoctorRepository.GetDoctorsForDate(doctors, date);
            datedDoctors.ForEach(d =>
                freeTimes.AddRange(GetFreeTimeIntervalsForDoctor(d, date, consiliumDuration)));
            return freeTimes;
        }

        private Overlapper GetDateOverlapper(List<List<TimeIntervalDoctor>> allPossibleSets, int schedulerDoctor,
            int consiliumDuration)
        {
            Overlapper dateBest = new Overlapper(0, new List<int>(), new TimeInterval());
            foreach (List<TimeIntervalDoctor> possibleSet in allPossibleSets)
            {
                if (possibleSet.Count <= dateBest.OverlappedNumber)
                    continue;

                Overlapper setBest = GetSetOverlapper(possibleSet, consiliumDuration, schedulerDoctor);

                if (setBest.OverlappedNumber > dateBest.OverlappedNumber)
                    dateBest = setBest;
            }

            return dateBest;
        }

        private Overlapper GetSetOverlapper(List<TimeIntervalDoctor> possibleSet, int consiliumDuration,
            int schedulerDoctor)
        {
            Overlapper setBest = new Overlapper(0, new List<int>(), new TimeInterval());
            foreach (TimeIntervalDoctor starter in possibleSet)
            {
                bool isSchedulerPresent = false;

                Overlapper epochBest = GetEpochOverlapper(starter, consiliumDuration, possibleSet,
                    schedulerDoctor, ref isSchedulerPresent);

                if (epochBest.OverlappedNumber > setBest.OverlappedNumber && isSchedulerPresent)
                    setBest = epochBest;
            }

            return setBest;
        }

        private Overlapper GetEpochOverlapper(TimeIntervalDoctor starter, int consiliumDuration,
            List<TimeIntervalDoctor> possibleSet, int schedulerDoctor, ref bool isSchedulerPresent)
        {
            Overlapper epochBest = new Overlapper(0, new List<int>(),
                new TimeInterval(starter.Interval.Start, starter.Interval.Start.AddMinutes(consiliumDuration)));
            foreach (TimeIntervalDoctor sub in possibleSet)
            {
                if (!TimeInterval.IsIntervalInside(sub.Interval, epochBest.BestConsiliumTime))
                    continue;
                if (sub.DoctorId == schedulerDoctor)
                    isSchedulerPresent = true;

                epochBest.OverlappedNumber++;
                epochBest.OverlappedDoctorsList.Add(sub.DoctorId);
            }

            return epochBest;
        }

        private List<List<TimeIntervalDoctor>> GenerateFreeTimeSets(List<TimeIntervalDoctor> freeTimes,
            int schedulerDoctor)
        {
            List<List<TimeIntervalDoctor>> allMatchingSets = new List<List<TimeIntervalDoctor>>();

            foreach (TimeIntervalDoctor mainFreeTime in freeTimes)
            {
                bool isSchedulerPresent = mainFreeTime.DoctorId == schedulerDoctor;

                List<TimeIntervalDoctor> possibleSet = new List<TimeIntervalDoctor> { mainFreeTime };

                possibleSet.AddRange(FindSubFreeTimeStartInsideMe(mainFreeTime, freeTimes,
                    schedulerDoctor, ref isSchedulerPresent));

                if (isSchedulerPresent)
                    allMatchingSets.Add(possibleSet);
            }

            return allMatchingSets;
        }

        private List<TimeIntervalDoctor> FindSubFreeTimeStartInsideMe(TimeIntervalDoctor mainFreeTime,
            List<TimeIntervalDoctor> freeTimes, int schedulerDoctor, ref bool isSchedulerPresent)
        {
            List<TimeIntervalDoctor> validFreeTimes = new List<TimeIntervalDoctor>();
            foreach (TimeIntervalDoctor subFreeTime in freeTimes)
            {
                if (subFreeTime.DoctorId == mainFreeTime.DoctorId ||
                    !mainFreeTime.Interval.IsOtherStartWithingMe(subFreeTime.Interval))
                    continue;

                if (subFreeTime.DoctorId == schedulerDoctor)
                    isSchedulerPresent = true;

                validFreeTimes.Add(subFreeTime);
            }

            return validFreeTimes;
        }

        private List<TimeIntervalDoctor> GetFreeTimeIntervalsForDoctor(Doctor doctor, DateTime date, int consDuration)
        {
            DateTime freeStart = doctor.GetStartWorkingHoursDate(date);
            DateTime freeEnd = doctor.GetEndWorkingHoursDate(date);

            if (doctor.Appointments.FirstOrDefault() == null)
                return WholeDayFree(freeStart, freeEnd, doctor.Id);

            return CreateFreeTimeIntervals(freeStart, freeEnd, doctor, consDuration);
        }

        private List<TimeIntervalDoctor> CreateFreeTimeIntervals(DateTime freeStart, DateTime freeEnd, Doctor doctor,
            int consDuration)
        {
            List<TimeIntervalDoctor> freeIntervals = new List<TimeIntervalDoctor>();
            for (int i = 0; i <= doctor.Appointments.Count; i++)
            {
                TimeInterval freeTime;
                if (i != doctor.Appointments.Count)
                {
                    freeTime = new TimeInterval(freeStart, doctor.Appointments[i].StartAt);
                    freeStart = doctor.Appointments[i].EndAt;
                }
                else
                    freeTime = new TimeInterval(freeStart, freeEnd);

                var duration = freeTime.End.Subtract(freeTime.Start).TotalMinutes;
                if (duration > consDuration)
                    freeIntervals.Add(new TimeIntervalDoctor(freeTime, doctor.Id));
            }

            return freeIntervals;
        }

        private List<TimeIntervalDoctor> WholeDayFree(DateTime freeStart, DateTime freeEnd, int docId)
        {
            TimeInterval freeDay = new TimeInterval(freeStart, freeEnd);
            TimeIntervalDoctor freeTimeInterval = new TimeIntervalDoctor(freeDay, docId);
            return new List<TimeIntervalDoctor> { freeTimeInterval };
        }
    }
}