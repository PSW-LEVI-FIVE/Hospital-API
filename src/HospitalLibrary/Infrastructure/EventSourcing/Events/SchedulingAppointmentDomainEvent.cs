using System;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Patients;

namespace HospitalLibrary.Infrastructure.EventSourcing.Events
{
    public enum SchedulingAppointmentEventType { STARTED, PICKED_DATE, PICKED_SPECIALITY, PICKED_DOCTOR, PICKED_TIME,  CANCELED, FINISHED }

    public class SchedulingAppointmentDomainEvenet : DomainEvent
    {
        public SchedulingAppointmentEventType Type { get; set; }

        public SchedulingAppointmentDomainEvenet(int aggregateId, DateTime timestamp,SchedulingAppointmentEventType type) : base(aggregateId, timestamp)
        {
            Type = type;
        }
    }
}