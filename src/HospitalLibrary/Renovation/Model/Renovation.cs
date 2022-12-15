using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Renovation.Model
{
    public class Renovation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [ForeignKey("MainRoomId")] public int MainRoomId { get; set; }

        public Room MainRoom { get; set; }

        [ForeignKey("SecondaryRoomId")] public int SecondaryRoomId { get; set; }
        public Room SecondaryRoom { get; set; }
        public RenovationType Type { get; set; }
        public RenovationState State { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

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
