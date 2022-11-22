using HospitalLibrary.Floors;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Map;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Floors;

public class CreateFloorTests
{
    [Fact]
    public void Creates_floor_successfully()
    {
        var floorRepository = new Mock<IFloorRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        
        unitOfWork.Setup(u => u.FloorRepository).Returns(floorRepository.Object);
        unitOfWork.Setup(u => u.FloorRepository.Add(It.IsAny<Floor>())).Verifiable();
        unitOfWork.Setup(u => u.FloorRepository.Save()).Verifiable();

        var floorService = new FloorService(unitOfWork.Object);

        Floor floor = new Floor()
        {
            Number = 1,
            Area = 150
        };

        var result = floorService.Create(floor);
        result.ShouldNotBeNull();
    }

    [Fact]
    public void Creates_map_floor_successfully()
    {
        var mapFloorRepository = new Mock<IMapFloorRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        
        unitOfWork.Setup(u => u.MapFloorRepository).Returns(mapFloorRepository.Object);
        unitOfWork.Setup(u => u.MapFloorRepository.Add(It.IsAny<MapFloor>())).Verifiable();
        unitOfWork.Setup(u => u.MapFloorRepository.Save()).Verifiable();

        var mapService = new MapService(unitOfWork.Object);
            
        MapFloor floor = new MapFloor()
        {
            MapBuildingId = 2,
            Width = 100,
            Height = 100
        };

        var result = mapService.CreateFloor(floor);
        result.ShouldNotBeNull();
    }
}