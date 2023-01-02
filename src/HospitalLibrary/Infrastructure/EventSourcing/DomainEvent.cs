using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalLibrary.Infrastructure.EventSourcing
{
    public class DomainEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AggregateId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Name { get; private set; }

        public DomainEvent(int aggregateId, DateTime timestamp, string name)
        {
            AggregateId = aggregateId;
            Timestamp = timestamp;
            Name = name;
        }
    }
}