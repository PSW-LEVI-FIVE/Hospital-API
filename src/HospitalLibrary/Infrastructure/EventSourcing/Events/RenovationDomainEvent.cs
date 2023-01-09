using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Infrastructure.EventSourcing.Events
{
  public enum RenovationEventType{STARTED, ADDED_BASIC_INFO, TIME_CHOSEN, ADDED_ADDITION_INFO, CANCELED, FINISHED}

  public class RenovationDomainEvent:DomainEvent
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
