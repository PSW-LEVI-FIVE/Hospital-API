using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Floors;

public enum RoomType
{
    OPERATION_ROOM,
    EXAMINATION_ROOM,
    HOSPITAL_ROOM,
    CAFETERIA
}
namespace HospitalLibrary.Rooms.Model
{
    public class Room
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        public string RoomNumber { get; set; }
        public float Area { get; set; }

        [ForeignKey("Floor")]
        public int FloorId { get; set; }
        public Floor Floor { get; set; }

        public RoomType RoomType { get; set; }
        private List<RoomEquipment> RoomEquipment { get; set; }


        public Room() {}
    }
}