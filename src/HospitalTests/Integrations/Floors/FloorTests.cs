using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Floors;
using HospitalLibrary.Floors.Dtos;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Map;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Floors;

public class FloorTests: BaseIntegrationTest
{
    public FloorTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public void Creates_floor()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new MapController(
            scope.ServiceProvider.GetRequiredService<IMapService>(),
            scope.ServiceProvider.GetRequiredService<IRoomService>(),
            scope.ServiceProvider.GetRequiredService<IFloorService>(),
            scope.ServiceProvider.GetRequiredService<IBuildingService>()
            );

        var dto = new CreateFloorDto()
        {
            BuildingId = 2,
            Area = 150,
            Height = 100,
            Width = 100,
            XCoordinate = 150,
            YCoordinate = 150,
            Number = 1,
            RgbColour = "#FFFFFF"
        };

        var result = ((OkObjectResult)controller.CreateFloor(dto)).Value as MapFloor;
        result.ShouldNotBeNull();

    }
}