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
            List<int> overallOverlapedDoctors = new List<int>();
            TimeInterval overallBestConsiliumTime=null; 
            
            foreach (DateTime date in dates)
            {
                Console.WriteLine("Novi Datum");
                Console.WriteLine(date);
                Doctor doctorScheduler = null;
                IEnumerable<Doctor> datedDoctors = _unitOfWork.DoctorRepository.GetDoctorsForDate(doctors, date);
                List<TimeIntervalDoctor> freeTimes = new List<TimeIntervalDoctor>();
                
                //Load all free times for doctors
                foreach (Doctor doc in datedDoctors)
                {
                    Console.WriteLine(doc.Id);
                    Console.WriteLine(doc.Name);
                    Console.WriteLine(doc.Surname);
                    List<TimeIntervalDoctor> freeTime =
                        GetFreeTimeIntervalsForDoctor(doc, date, consiliumDuration, doc.Id);
                    foreach (TimeIntervalDoctor time in freeTime)
                    {
                        //OBIRASTI OVU FOR PETLJU, SLUZI SAMO ZA ISPIS
                        Console.WriteLine(time.Interval);
                    }
                    freeTimes.AddRange(freeTime);
                }

                //Generate all possible sets
                List<List<TimeIntervalDoctor>> allPossibleSets = new List<List<TimeIntervalDoctor>>();
                foreach (TimeIntervalDoctor timeDoc in freeTimes)
                {
                    bool isSchedulerPresent = timeDoc.DoctorId == schedulerDoctor;
                    
                    List<TimeIntervalDoctor> set = new List<TimeIntervalDoctor>();
                    set.Add(timeDoc);
                    foreach (TimeIntervalDoctor subTimeDoc in freeTimes)
                    {
                        if (subTimeDoc.DoctorId == timeDoc.DoctorId)
                            continue;

                        if (!timeDoc.Interval.IsOtherStartWithingMe(subTimeDoc.Interval))
                            continue;

                        if (subTimeDoc.DoctorId == schedulerDoctor)
                            isSchedulerPresent = true;
                        
                        set.Add(subTimeDoc);
                    }
                    
                    if(isSchedulerPresent)
                        allPossibleSets.Add(set);
                }

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
                        TimeInterval possibleConsilum = new TimeInterval(starter.Interval.Start,
                            starter.Interval.Start.AddMinutes(consiliumDuration));
                        int epochMaximumOverlaps = 0;
                        bool isSchedulerPresent = false;
                        List<int> epochOverlapedDoctors = new List<int>();
                        
                        foreach (TimeIntervalDoctor sub in possibleSet)
                        {
                            if (TimeInterval.IsIntervalInside(sub.Interval, possibleConsilum))
                            {
                                if (sub.DoctorId == schedulerDoctor)
                                    isSchedulerPresent = true;
                                epochMaximumOverlaps++;
                                epochOverlapedDoctors.Add(sub.DoctorId);
                            }
                        }

                        if (epochMaximumOverlaps > setMaximumOverlaps && isSchedulerPresent)
                        {
                            setBestConsiliumTime = possibleConsilum;
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
                    overallOverlapedDoctors = dateOverlapedDoctors;
                }
                
                Console.WriteLine("********");
            }

            Console.WriteLine("==========");
            GetBestConsiliumsDTO cons = new GetBestConsiliumsDTO(overallBestConsiliumTime.Start,overallBestConsiliumTime.End,
                overallOverlapedDoctors,schedulerDoctor,consiliumDuration);
            return cons;
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