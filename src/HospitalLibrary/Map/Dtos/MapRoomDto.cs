using System.Collections.Generic;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Map.Dtos
{
    public class MapRoomDto
    {
        public int Id { get; set; }
        public int MapFloorId { get; set; }
        public int RoomId { get; set; }
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public string RbgColour { get; set; }
        public List<Coordinates> SecondaryCoordinates { get; set; }
        
        public MapRoomDto(MapRoom mapRoom)
        {
            Id = mapRoom.Id;
            MapFloorId = mapRoom.MapFloorId;
            RoomId = mapRoom.RoomId;
            XCoordinate = mapRoom.Coordinates.XCoordinate;
            YCoordinate = mapRoom.Coordinates.YCoordinate;
            Width = mapRoom.Coordinates.Width;
            Height = mapRoom.Coordinates.Height;
            RbgColour = mapRoom.RbgColour;
            SecondaryCoordinates = mapRoom.SecondaryCoordinatesList != null ? mapRoom.SecondaryCoordinatesList.Coordinates : null;
        }
    }
}