using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Rooms;

namespace HospitalLibrary.Map
{
    public class MapRoom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        [ForeignKey("MapFloor")]
        public int MapFloorId { get; set; }
        public virtual MapFloor MapFloor { get; set; }
        
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public string RbgColour { get; set; }
        
        public MapRoom() {}
    }
}