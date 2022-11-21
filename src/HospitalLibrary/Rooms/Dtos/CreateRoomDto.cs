using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Map;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Dtos
{
    public class CreateRoomDto
    {
        [Required]
        public string RoomNumber { get; set; }
        [Required]
        public float Area { get; set; }
        [Required]
        public int MapFloorId { get; set; }
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


        public Room DtoToRoom()
        {
            return new Room()
            {
                RoomNumber = RoomNumber,
                Area = Area
            };
        }

        public MapRoom DtoToMapRoom()
        {
            return new MapRoom()
            {
                MapFloorId = MapFloorId,
                XCoordinate = XCoordinate,
                YCoordinate = YCoordinate,
                Width = Width,
                Height = Height,
                RbgColour = RgbColour
            };
        }
        
    }
}