using System;
using HospitalLibrary.Appointments.Dtos;

namespace HospitalLibrary.Appointments
{
    public class CalendarInterval
    {
        public TimeOfDayDTO StartsAt { get; set; }
        public TimeOfDayDTO EndsAt { get; set; }
        public string? Patient { get; set; }
        public AppointmentType Type { get; set; }
        public int Id { get; set; }


        public CalendarInterval(TimeOfDayDTO startsAt, TimeOfDayDTO endsAt, string patient, int id)
        {
            StartsAt = startsAt;
            EndsAt = endsAt;
            Patient = patient;
            Id = id;
        }
        
        public CalendarInterval(TimeOfDayDTO startsAt, TimeOfDayDTO endsAt, string patient, AppointmentType type, int id)
        {
            StartsAt = startsAt;
            EndsAt = endsAt;
            Patient = patient;
            Id = id;
            Type = type;
        }
        
        public CalendarInterval() {}
    }
}