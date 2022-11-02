using System;

namespace HospitalLibrary.Appointments
{
    public class CalendarInterval
    {
        public TimeSpan StartsAt { get; set; }
        public TimeSpan EndsAt { get; set; }
        public string Patient { get; set; }
        
        public int Id { get; set; }


        public CalendarInterval(TimeSpan startsAt, TimeSpan endsAt, string patient, int id)
        {
            StartsAt = startsAt;
            EndsAt = endsAt;
            Patient = patient;
            Id = id;
        }
        
        public CalendarInterval() {}
    }
}