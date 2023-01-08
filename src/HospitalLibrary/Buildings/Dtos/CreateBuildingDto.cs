using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Map;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Buildings.Dtos
{
    public class CreateBuildingDto
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float XCoordinate { get; set; }
        [Required]
        public float YCoordinate { get; set; }
        [Required]
        public float Width { get; set; }
        [Required]
        public float Height { get; set; }
        [Required]
        public string RgbColour { get; set; }

        public Building DtoToBuilding()
        {
            return new Building()
            {
                Address = Address,
                Name = Name
            };
        }

        public MapBuilding DtoToMapBuilding()
        {
            return new MapBuilding()
            {
                Coordinates = new Coordinates(XCoordinate, YCoordinate, Width, Height),
                RgbColour = RgbColour
            };
        }
    }
}