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
        
        public string Shape { get; set; }
        public string RgbColour { get; set; }
        
        public virtual ICollection<MapRoom> MapRooms { get; set; }

        public MapFloor() {}
    }
}