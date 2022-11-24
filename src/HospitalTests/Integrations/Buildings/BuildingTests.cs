using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Buildings.Dtos;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Map;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Buildings;

[Collection("Test")]
public class BuildingTests: BaseIntegrationTest
{
    public BuildingTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public void Creates_building()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new MapController(
            scope.ServiceProvider.GetRequiredService<IMapService>(),
            scope.ServiceProvider.GetRequiredService<IRoomService>(),
            scope.ServiceProvider.GetRequiredService<IFloorService>(),
            scope.ServiceProvider.GetRequiredService<IBuildingService>()
            );

        var dto = new CreateBuildingDto()
        {
            Address = "Vojvode Misica 9",
            Name = "Block 17",
            XCoordinate = 50,
            YCoordinate = 50,
            Width = 50,
            Height = 50,
            RgbColour = "#FFFFFF"
        };

        var result = ((OkObjectResult)controller.CreateBuilding(dto)).Value as MapBuilding;
        result.ShouldNotBeNull();
    }
}