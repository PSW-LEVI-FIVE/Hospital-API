using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;


namespace HospitalLibrary.Rooms.Model
{
    public class EquipmentReallocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [ForeignKey("StartingRoom")]
        public int StartingRoomId { get; set; }

        public Room StartingRoom { get; set; }
        [ForeignKey("DestinationRoom")]
        public int DestinationRoomId { get; set; }

        public Room DestinationRoom { get; set; }

        [ForeignKey("Equipment")]
        public int EquipmentId { get; set; }
        public RoomEquipment Equipment { get; set; }

        public int amount { get; set; }
        public ReallocationState state { get; set; }
        
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        public EquipmentReallocation() 
        {
        }
        public EquipmentReallocation(int id, int startingRoomId, int destinationRoomId, int equipmentId, int amount, ReallocationState state, DateTime startAt, DateTime endAt)
        {
            Id = id;
            StartingRoomId = startingRoomId;
            DestinationRoomId = destinationRoomId;
            EquipmentId = equipmentId;
            this.amount = amount;
            this.state = state;
            StartAt = startAt;
            EndAt = endAt;
        }

        public TimeInterval GetInterval()
        {
          return new TimeInterval(StartAt, EndAt);
        }


        

    }
}

public enum ReallocationState 
{
    FINISHED,
    PENDING,
    CANCELED
}
