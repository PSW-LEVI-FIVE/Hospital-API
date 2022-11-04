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
        public Building Building { get; set; }
        
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        
        public string RgbColour { get; set; }
        
        
        public MapBuilding() {}
    }
}