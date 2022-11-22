using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Map;

namespace HospitalLibrary.Floors.Dtos
{
    public class CreateFloorDto
    {
        [Required]
        public int BuildingId;
        [Required]
        public int Number;
        [Required]
        public float Area;
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

        public Floor DtoToFloor()
        {
            return new Floor()
            {
                Number = Number,
                Area = Area
            };
        }

        public MapFloor DtoToMapFloor()
        {
            return new MapFloor()
            {
                XCoordinate = XCoordinate,
                YCoordinate = YCoordinate,
                Width = Width,
                Height = Height,
                RgbColour = RgbColour,
                MapBuildingId = BuildingId
            };  
        }
    }
}