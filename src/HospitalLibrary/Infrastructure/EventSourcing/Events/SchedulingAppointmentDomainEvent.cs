using System;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Appointments;
using HospitalLibrary.Patients;

namespace HospitalLibrary.Infrastructure.EventSourcing.Events
{
    public enum SchedulingAppointmentEventType { STARTED, PICKED_DATE, PICKED_SPECIALITY, PICKED_DOCTOR, PICKED_TIME, FINISHED }

    public class SchedulingAppointmentDomainEvent : DomainEvent
    {
        public SchedulingAppointmentEventType Type { get; set; }
        public SchedulingAppointmentDomainEvent(int aggregateId, DateTime timestamp,SchedulingAppointmentEventType type) : base(aggregateId, timestamp)
        {
            Type = type;
        }
    }
}