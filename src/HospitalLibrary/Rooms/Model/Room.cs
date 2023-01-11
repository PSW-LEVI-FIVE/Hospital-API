using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Floors;

using HospitalLibrary.Shared.Model.ValueObjects;

using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Model;


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
    public class Room : BaseEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        public Area Area { get; set; }
        public string RoomNumber { get; private set; }
       


        [ForeignKey("Floor")]
        public int FloorId { get; private set; }
        public Floor Floor { get; private set; }

        public RoomType RoomType { get; private set; }
        public List<RoomEquipment> RoomEquipment { get; private set; }

        public Room() {}

        public Room(int id, string roomNumber, Area area, int floorId, RoomType roomType)
        {
            Id = id;
            RoomNumber = roomNumber;
            Area = area;
            FloorId = floorId;
            
            RoomType = roomType;
        }

        public Room(string roomNumber, Area area, RoomType roomType)
        {
            RoomNumber = roomNumber;
            Area = area;
            RoomType = roomType;
        }
        public Room(int id, string roomNumber, RoomType roomType)
        {
            Id = id;
            RoomNumber = roomNumber;
            RoomType = roomType;
        }

        public Room(string roomNumber, Area area)
        {
            RoomNumber = roomNumber;
            Area = area;
        }

        public Room(int id)
        {
            Id = id;
        }
        public Room(string roomNumber, Area area, int floorId, RoomType roomType)
        {
            RoomNumber = roomNumber;
            Area = area;
            FloorId = floorId;
            RoomType = roomType;
        }
        public Room(string roomNumber, float area, int floorId, RoomType roomType)
        {
            RoomNumber = roomNumber;
            Area = new Area(area);
            FloorId = floorId;
            RoomType = roomType;
        }

        public void UpdateArea(Area area)
        {
            if (area.Measure < 0)
                throw new BadRequestException(("Area can't be negative"));
            Area = area;
        }
        public void UpdateName(string name)
        {
            if (name == null || name.Trim().Equals(""))
                throw new BadRequestException("You must enter new name!");
            RoomNumber = name;
        }
    }
}