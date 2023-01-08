namespace HospitalLibrary.Map.Dtos
{
    public class MapFloorDto
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
        public int MapBuildingId { get; set; }
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public string RgbColour { get; set; }
        
        public MapFloorDto(MapFloor mapFloor)
        {
            Id = mapFloor.Id;
            FloorId = mapFloor.FloorId;
            MapBuildingId = mapFloor.MapBuildingId;
            XCoordinate = mapFloor.Coordinates.XCoordinate;
            YCoordinate = mapFloor.Coordinates.YCoordinate;
            Width = mapFloor.Coordinates.Width;
            Height = mapFloor.Coordinates.Height;
            RgbColour = mapFloor.RgbColour;
        }
    }
}