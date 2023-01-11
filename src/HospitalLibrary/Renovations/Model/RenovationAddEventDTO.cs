using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Infrastructure.EventSourcing.Events;

namespace HospitalLibrary.Renovations.Model
{
  public class RenovationAddEventDTO
  {
    public RenovationEventType EventType { get; set; }
    public RenovationType Type { get; set; }
    public DateTime Time { get; set; }
    public string Uuid { get; set; }
    public int RenovationId { get; set; }

    public RenovationDomainEvent MapToModel()
    {
      return new RenovationDomainEvent(RenovationId, DateTime.Now, EventType, Type, Uuid);
    }
  }
}
