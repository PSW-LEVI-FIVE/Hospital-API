namespace HospitalLibrary.Map.Dtos
{
    public class MapBuildingDto
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public float XCoordinate { get; set; }
        public float YCoordinate { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public string RgbColour { get; set; }

        public MapBuildingDto(MapBuilding mapBuilding)
        {
            Id = mapBuilding.Id;
            BuildingId = mapBuilding.BuildingId;
            XCoordinate = mapBuilding.Coordinates.XCoordinate;
            YCoordinate = mapBuilding.Coordinates.YCoordinate;
            Width = mapBuilding.Coordinates.Width;
            Height = mapBuilding.Coordinates.Height;
            RgbColour = mapBuilding.RgbColour;
        }
    }
}