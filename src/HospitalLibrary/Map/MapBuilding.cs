using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Buildings;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Map
{
    public class MapBuilding
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        
        [ForeignKey("Building")]
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        
        public Coordinates Coordinates { get; set; }
      
        public string RgbColour { get; set; }
        
        
        public MapBuilding() {}
    }
}