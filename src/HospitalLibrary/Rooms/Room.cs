using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Floors;

namespace HospitalLibrary.Rooms
{
    public class Room
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        public string RoomNumber { get; set; }

        [ForeignKey("Floor")]
        public int FloorId { get; set; }
        public virtual Floor Floor { get; set; }


        public Room() {}
    }
}