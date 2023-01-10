using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Floors;
using HospitalLibrary.Shared.Model.ValueObjects;

public enum RoomType
{
    NO_TYPE,
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
        public Area Area { get; set; }

        [ForeignKey("Floor")]
        public int FloorId { get; set; }
        public Floor Floor { get; set; }

        public RoomType RoomType { get; set; }
        public List<RoomEquipment> RoomEquipment { get; set; }

        public Room() {}

        public Room(int id, string roomNumber, Area area, int floorId, RoomType roomType)
        {
            Id = id;
            RoomNumber = roomNumber;
            Area = area;
            FloorId = floorId;
            
            RoomType = roomType;
        }


    }
}