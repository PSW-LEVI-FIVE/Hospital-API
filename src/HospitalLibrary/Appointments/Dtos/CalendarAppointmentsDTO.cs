using System;
using System.Collections.Generic;

namespace HospitalLibrary.Appointments.Dtos
{
    public class CalendarAppointmentsDTO
    {
        public string Date { get; set; }
        public IEnumerable<CalendarInterval> Intervals { get; set; }

        public CalendarAppointmentsDTO(string date, IEnumerable<CalendarInterval> intervals)
        {
            Date = date;
            Intervals = intervals;
        }
    }
}