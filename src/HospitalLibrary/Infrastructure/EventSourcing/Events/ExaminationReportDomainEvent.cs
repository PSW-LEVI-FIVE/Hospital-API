﻿using System;

namespace HospitalLibrary.Infrastructure.EventSourcing.Events
{
    public enum ExaminationReportEventType { STARTED, FINISHED_SYMPTOMS, FINISHED_REPORT, FINISHED_PRESCRPITION, FINISHED }
    
    public class ExaminationReportDomainEvent: DomainEvent
    {
        public ExaminationReportEventType Type { get; private set; }

        public string Uuid { get; private set; }
        
        public ExaminationReportDomainEvent(int aggregateId, DateTime timestamp, ExaminationReportEventType type) : base(aggregateId, timestamp)
        {
            Type = type;
        }
        
        public ExaminationReportDomainEvent(int aggregateId, DateTime timestamp, ExaminationReportEventType type, string uuid) : base(aggregateId, timestamp)
        {
            Type = type;
            Uuid = uuid;
        }
    }
}