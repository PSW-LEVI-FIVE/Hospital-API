using System;

namespace HospitalLibrary.Appointments.Dtos
{
    public class TimeOfDayDTO
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        
        public TimeOfDayDTO(TimeSpan timeOfDay)
        {
            Hours = timeOfDay.Hours;
            Minutes = timeOfDay.Minutes;
            Seconds = timeOfDay.Seconds;
        }
    }
}