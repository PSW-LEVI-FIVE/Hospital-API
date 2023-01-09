using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Infrastructure.EventSourcing.Events
{
  public enum RenovationEventType{STARTED, FINISHED_BASIC_INFO, FINISHED_TIMEINTERVAL, FINISHED_ADDING_ADDITION_INFO, CANCELED, FINISHED}
  internal class RenovationDomainEvent:DomainEvent
  {
    public RenovationEventType EventType { get; set; }
    public RenovationType RenovationType { get; set; }
    public string Uuid { get; private set; }

    public RenovationDomainEvent(int aggregateId, DateTime timestamp, RenovationEventType eventType,RenovationType renovationType, string uuid) : base(aggregateId, timestamp)
    {
      EventType = eventType;
      RenovationType=renovationType;
      Uuid = uuid;
    }
  }
}
