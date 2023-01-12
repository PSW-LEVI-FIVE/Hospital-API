using System;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments.Dtos
{
    public class SchedulePerAgeDTO
    {
        public double Time { get; set; }

        public SchedulePerAgeDTO(double time)
        {
            Time = time;
        }
    }
}