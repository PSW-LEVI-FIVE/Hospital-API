using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Map
{
    public class MapRoom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        [ForeignKey("MapFloor")]
        public int MapFloorId { get; set; }
        public MapFloor MapFloor { get; set; }
        
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        
        public Coordinates Coordinates { get; set; }

        public string RbgColour { get; set; }
        
        public MapRoom() {}
    }
}