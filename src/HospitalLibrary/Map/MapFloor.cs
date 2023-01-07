using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Floors;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Map
{
    public class MapFloor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        [ForeignKey("Floor")]
        public int FloorId { get; set; }
        public Floor Floor { get; set; }
        
        [ForeignKey("MapBuilding")]
        public int MapBuildingId { get; set; }
        public MapBuilding MapBuilding { get; set; }
        
       public Coordinates Coordinates { get; set; }

        public string RgbColour { get; set; }
        
        public virtual ICollection<MapRoom> MapRooms { get; set; }

        public MapFloor() {}
    }
}