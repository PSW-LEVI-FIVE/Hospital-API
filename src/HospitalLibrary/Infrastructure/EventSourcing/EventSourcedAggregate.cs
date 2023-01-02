using System.Collections.Generic;
using HospitalLibrary.Shared.Model;

namespace HospitalLibrary.Infrastructure.EventSourcing
{
    public abstract class EventSourcedAggregate: BaseEntity
    {
        public List<DomainEvent> Changes { get; protected set; }
        public int Version { get; protected set; }

        protected EventSourcedAggregate()
        {
            Changes = new List<DomainEvent>();
        }

        public abstract void Apply(DomainEvent changes);
    }
}