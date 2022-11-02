using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Floors;

namespace HospitalLibrary.Map
{
    public class MapFloor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        [ForeignKey("Floor")]
        public int FloorId { get; set; }
        public virtual Floor Floor { get; set; }
        
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public string RgbColour { get; set; }
        
        public virtual ICollection<MapRoom> MapRooms { get; set; }

        public MapFloor() {}
    }
}