using HospitalLibrary.Buildings;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Buildings;

public class CreateBuilding
{
    [Fact]
    public void Creates_building_successfully()
    {
        var buildingrepository = new Mock<IBuildingRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        
        unitOfWork.Setup(u => u.BuildingRepository).Returns(buildingrepository.Object);
        unitOfWork.Setup(u => u.BuildingRepository.Add(It.IsAny<Building>())).Verifiable();
        unitOfWork.Setup(u => u.BuildingRepository.Save()).Verifiable();

        var buildingService = new BuildingService(unitOfWork.Object);

        Building building = new Building()
        {
            Address = "Vojvode Misica 9",
            Name = "Block A"
        };

        var result = buildingService.Create(building);
        result.ShouldNotBeNull();
    }
    
}