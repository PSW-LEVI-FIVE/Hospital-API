using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Buildings.Dtos;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Map;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Buildings;

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
            scope.ServiceProvider.GetRequiredService<IFloorService>()
            );

        var dto = new CreateBuildingDto();

        var result = ((OkObjectResult)controller.CreateBuilding(dto)).Value as MapRoom;
        result.ShouldNotBeNull();
    }
}