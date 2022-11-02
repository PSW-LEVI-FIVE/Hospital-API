using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Buildings;

namespace HospitalLibrary.Map
{
    public class MapBuilding
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        [ForeignKey("Building")]
        public int BuildingId { get; set; }
        public virtual Building Building { get; set; }
        
        public string Coordinates { get; set; }
        public string Shape { get; set; }
        public string RgbColour { get; set; }
        
        
        public MapBuilding() {}
    }
}