using System.ComponentModel.DataAnnotations;
using HospitalLibrary.Map;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Rooms.Dtos
{
    public class CreateRoomDto
    {
        [Required]
        public string RoomNumber { get; set; }
        [Required]
        public float Area { get; set; }
        [Required]
        public RoomType RoomType { get; set; }
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


      

        public  Room DtoToRoom(int floorId)
        {
            return new Room(RoomNumber,Area,floorId, RoomType);
        }
        public MapRoom DtoToMapRoom()
        {
            return new MapRoom()
            {
                MapFloorId = MapFloorId,
                Coordinates = new Coordinates(XCoordinate, YCoordinate, Width, Height),
                RbgColour = RgbColour
            };
        }
        
    }
}