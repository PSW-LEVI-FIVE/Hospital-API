using System;

namespace HospitalLibrary.Infrastructure.EventSourcing.Events
{
    public enum ExaminationReportEventType { STARTED, ADDED_SYMPTOM, ADDED_REPORT, ADDED_PRESCRIPTION, FINISHED }
    
    public class ExaminationReportDomainEvent: DomainEvent
    {
        public ExaminationReportEventType Type { get; private set; }
        
        public ExaminationReportDomainEvent(int aggregateId, DateTime timestamp, string name, ExaminationReportEventType type) : base(aggregateId, timestamp, name)
        {
            Type = type;
        }
    }
}