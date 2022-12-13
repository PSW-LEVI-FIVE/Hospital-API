using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Consiliums.Dtos;
using HospitalLibrary.Consiliums.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Interfaces;
using LinqKit;

namespace HospitalLibrary.Consiliums
{
    public class ConsiliumService : IConsiliumService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsiliumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Consilium> Create(Appointment appointment, List<int> doctors)
        {
            Consilium consilium = appointment.Consilium;
            _unitOfWork.ConsiliumRepository.Add(consilium);
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
            List<DateTime> dates = timeInterval.GetDatesForDateRange();
            int overallDoctorsOverlap = 0;
            List<int> overallOverlappedDoctors = new List<int>();
            TimeInterval overallBestConsiliumTime = null;

            foreach (DateTime date in dates)
            {
                IEnumerable<Doctor> datedDoctors = _unitOfWork.DoctorRepository.GetDoctorsForDate(doctors, date);
                List<TimeIntervalDoctor> freeTimes = new List<TimeIntervalDoctor>();

                datedDoctors.ForEach(d =>
                    freeTimes.AddRange(GetFreeTimeIntervalsForDoctor(d, date, consiliumDuration, d.Id)));

                List<List<TimeIntervalDoctor>> allPossibleSets = GenerateFreeTimeSets(freeTimes, schedulerDoctor);


                //Generate the best possible consilium for date
                int dateDoctorsOverlap = 0;
                List<int> dateOverlapedDoctors = new List<int>();
                TimeInterval dateBestConsiliumTime = null;
                foreach (List<TimeIntervalDoctor> possibleSet in allPossibleSets)
                {
                    //Ako je nasao 3 doktora da se preklapaju nema smisla da gleda SubSetove sa 3 i manje preklapajuca
                    if (possibleSet.Count <= dateDoctorsOverlap && dateDoctorsOverlap != 0) //Pitanje jel mi treba 0?
                        continue;

                    int setMaximumOverlaps = 0;
                    TimeInterval setBestConsiliumTime = null;
                    List<int> setOverlapedDoctors = new List<int>();

                    //Every starter inside a set is a separate epoch
                    foreach (TimeIntervalDoctor starter in possibleSet)
                    {
                        TimeInterval possibleConsilium = new TimeInterval(starter.Interval.Start,
                            starter.Interval.Start.AddMinutes(consiliumDuration));
                        int epochMaximumOverlaps = 0;
                        bool isSchedulerPresent = false;
                        List<int> epochOverlapedDoctors = new List<int>();


                        foreach (TimeIntervalDoctor sub in possibleSet)
                        {
                            if (TimeInterval.IsIntervalInside(sub.Interval, possibleConsilium))
                            {
                                if (sub.DoctorId == schedulerDoctor)
                                    isSchedulerPresent = true;
                                epochMaximumOverlaps++;
                                epochOverlapedDoctors.Add(sub.DoctorId);
                            }
                        }


                        if (epochMaximumOverlaps > setMaximumOverlaps && isSchedulerPresent)
                        {
                            setBestConsiliumTime = possibleConsilium;
                            setMaximumOverlaps = epochMaximumOverlaps;
                            setOverlapedDoctors = epochOverlapedDoctors;
                        }
                    }

                    if (setMaximumOverlaps > dateDoctorsOverlap)
                    {
                        dateDoctorsOverlap = setMaximumOverlaps;
                        dateBestConsiliumTime = setBestConsiliumTime;
                        dateOverlapedDoctors = setOverlapedDoctors;
                    }
                }

                if (dateDoctorsOverlap > overallDoctorsOverlap)
                {
                    overallDoctorsOverlap = dateDoctorsOverlap;
                    overallBestConsiliumTime = dateBestConsiliumTime;
                    overallOverlappedDoctors = dateOverlapedDoctors;
                }
            }

            GetBestConsiliumsDTO cons = new GetBestConsiliumsDTO(overallBestConsiliumTime.Start,
                overallBestConsiliumTime.End,
                overallOverlappedDoctors, schedulerDoctor, consiliumDuration);
            return cons;
        }

        private List<List<TimeIntervalDoctor>> GenerateFreeTimeSets(List<TimeIntervalDoctor> freeTimes,
            int schedulerDoctor)
        {
            List<List<TimeIntervalDoctor>> allMatchingSets = new List<List<TimeIntervalDoctor>>();

            foreach (TimeIntervalDoctor freeTime in freeTimes)
            {
                bool isSchedulerPresent = freeTime.DoctorId == schedulerDoctor;

                List<TimeIntervalDoctor> possibleSet = new List<TimeIntervalDoctor>();
                possibleSet.Add(freeTime);

                foreach (TimeIntervalDoctor sampleFreeTime in freeTimes)
                {
                    if (sampleFreeTime.DoctorId == freeTime.DoctorId)
                        continue;

                    if (!freeTime.Interval.IsOtherStartWithingMe(sampleFreeTime.Interval))
                        continue;

                    if (sampleFreeTime.DoctorId == schedulerDoctor)
                        isSchedulerPresent = true;

                    possibleSet.Add(sampleFreeTime);
                }

                if (isSchedulerPresent)
                    allMatchingSets.Add(possibleSet);
            }

            return allMatchingSets;
        }

        private List<TimeIntervalDoctor> GetFreeTimeIntervalsForDoctor(Doctor doctor, DateTime date, int consDuration,
            int docId)
        {
            List<TimeIntervalDoctor> freeIntervals = new List<TimeIntervalDoctor>();

            Appointment firstApp = doctor.Appointments.FirstOrDefault();
            var day = (int)date.DayOfWeek;
            TimeSpan startWork = doctor.WorkingHours.First(w => w.Day.Equals(day)).Start;
            TimeSpan endWork = doctor.WorkingHours.First(w => w.Day.Equals(day)).End;

            DateTime freeStart =
                new DateTime(date.Year, date.Month, date.Day, startWork.Hours, startWork.Minutes, 0);
            DateTime freeEnd =
                new DateTime(date.Year, date.Month, date.Day, endWork.Hours, endWork.Minutes, 0);

            if (firstApp == null) //AKO JE SLOBODAN CEO DAN!
            {
                TimeInterval freeDay = new TimeInterval(freeStart, freeEnd);
                freeIntervals.Add(new TimeIntervalDoctor(freeDay, docId));
                return freeIntervals;
            }

            //AKO NIJE ITERIRAMO KROZ APPOINTMENTE I PRAVIMO SLOBODNE DANE
            TimeInterval freeTime;
            foreach (Appointment appointment in doctor.Appointments)
            {
                freeTime = new TimeInterval(freeStart, appointment.StartAt);
                freeStart = appointment.EndAt;
                var duration = freeTime.End.Subtract(freeTime.Start).TotalMinutes;
                if (duration > consDuration)
                    freeIntervals.Add(new TimeIntervalDoctor(freeTime, docId));
            }

            freeTime = new TimeInterval(freeStart, freeEnd);
            var duration1 = freeTime.End.Subtract(freeTime.Start).TotalMinutes;
            if (duration1 > consDuration)
                freeIntervals.Add(new TimeIntervalDoctor(freeTime, docId));

            return freeIntervals;
        }
    }
}