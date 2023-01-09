using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Rooms.Model;
using Microsoft.VisualBasic;
using HospitalLibrary.Infrastructure.EventSourcing;
using NUnit.Framework.Internal.Execution;
using HospitalLibrary.Examination;
using SendGrid.Helpers.Mail;
using System.Security.Policy;

namespace HospitalLibrary.Renovations.Model
{
  public class Renovation : EventSourcedAggregate
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }

    [ForeignKey("MainRoomId")] public int MainRoomId { get; set; }

    public Room MainRoom { get; set; }

    [ForeignKey("SecondaryRoomId")] public int? SecondaryRoomId { get; set; }
    public Room? SecondaryRoom { get; set; }
    public RenovationType Type { get; set; }
    public RenovationState State { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public String? roomName { get; set; }

    public Renovation()
    {
    }

    public Renovation(int id, int mainRoomId, DateTime startAt, DateTime endAt, Room secondaryRoom)
    {
      Id = id;
      MainRoomId = mainRoomId;
      StartAt = startAt;
      EndAt = endAt;
      State = RenovationState.PENDING;
      Type = RenovationType.SPLIT;
      SecondaryRoom = secondaryRoom;
    }
    public Renovation(int id, int mainRoomId, int secondaryRoomId, DateTime startAt, DateTime endAt)
    {
      Id = id;
      MainRoomId = mainRoomId;
      StartAt = startAt;
      EndAt = endAt;
      State = RenovationState.PENDING;
      Type = RenovationType.MERGE;
      SecondaryRoomId = secondaryRoomId;
    }
    public Renovation(int mainRoomId,RenovationType type)
    {
      MainRoomId = mainRoomId;
      Type = type;
    }

    public TimeInterval GetInterval()
    {
      return new TimeInterval(StartAt, EndAt);
    }

    public string GetDictionaryKet()
    {
      return "reno" + Id.ToString();
    }

    public override void Apply(DomainEvent @event)
    {
      Changes.Add(@event);
    }

    public void UpdateAdditional(Renovation renovation)
    {
      MainRoomId = renovation.MainRoomId;
      StartAt = renovation.StartAt;
      EndAt = renovation.EndAt;
      State = RenovationState.PENDING;
      Type = RenovationType.SPLIT;
      SecondaryRoomId = renovation.SecondaryRoomId;
      roomName = renovation.roomName;
    }
  }
}
public enum RenovationState
{
  FINISHED,
  PENDING,
  CANCELED
}
public enum RenovationType
{
  MERGE,
  SPLIT
}
