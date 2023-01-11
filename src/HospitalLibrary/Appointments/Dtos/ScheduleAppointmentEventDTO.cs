using System;
using HospitalLibrary.Infrastructure.EventSourcing.Events;

namespace HospitalLibrary.Appointments.Dtos
{
    public class ScheduleAppointmentEventDTO
    {
        public SchedulingAppointmentEventType EventType { get; set; }

        public DateTime Time { get; set; }
        
        public int AggregateId { get; set; }

        public SchedulingAppointmentDomainEvenet MapToModel()
        {
            return new SchedulingAppointmentDomainEvenet(AggregateId, Time, EventType);
        }
    }
}