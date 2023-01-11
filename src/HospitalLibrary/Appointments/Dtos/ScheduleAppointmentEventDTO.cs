using System;
using HospitalLibrary.Infrastructure.EventSourcing.Events;

namespace HospitalLibrary.Appointments.Dtos
{
    public class ScheduleAppointmentEventDTO
    {
        public SchedulingAppointmentEventType EventType { get; set; }

        public DateTime Time { get; set; }

        public SchedulingAppointmentDomainEvenet MapToModel()
        {
            return new SchedulingAppointmentDomainEvenet(1, Time, EventType);
        }
    }
}