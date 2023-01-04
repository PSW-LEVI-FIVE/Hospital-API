using System;
using HospitalLibrary.Infrastructure.EventSourcing.Events;

namespace HospitalLibrary.Examination.Dtos
{
    public class EventDTO
    {
        public ExaminationReportEventType EventType { get; set; }
        public DateTime Time { get; set; }
        public string Uuid { get; set; }
        public int ExaminationReportId { get; set; }

        public ExaminationReportDomainEvent MapToModel()
        {
            return new ExaminationReportDomainEvent(ExaminationReportId, Time, EventType, Uuid);
        }
    }
}