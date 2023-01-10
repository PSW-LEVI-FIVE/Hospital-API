using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Map;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Floors.Dtos
{
    public class CreateFloorDto
    {
        [Required]
        public int BuildingId;
        [Required]
        public int Number;
        [Required]
        public Area Area;
        [Required]
        public float XCoordinate;
        [Required]
        public float YCoordinate;
        [Required]
        public float Width;
        [Required]
        public float Height;
        [Required]
        public string RgbColour;

        public Floor DtoToFloor(int buildingId)
        {
            return new Floor(Number, Area,buildingId);
        }

        public MapFloor DtoToMapFloor()
        {
            return new MapFloor()
            {
                Coordinates = new Coordinates(XCoordinate, YCoordinate, Width, Height),
                RgbColour = RgbColour,
                MapBuildingId = BuildingId
            };  
        }
    }
}