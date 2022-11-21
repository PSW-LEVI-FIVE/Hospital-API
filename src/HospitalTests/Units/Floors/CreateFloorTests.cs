using HospitalLibrary.Floors;
using HospitalLibrary.Floors.Interfaces;
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
}