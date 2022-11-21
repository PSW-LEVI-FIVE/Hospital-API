using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Floors;
using HospitalLibrary.Floors.Dtos;
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
            scope.ServiceProvider.GetRequiredService<IRoomService>()
            );

        var dto = new CreateFloorDto();

        var result = ((OkObjectResult)controller.CreateFloor(dto)).Value as MapFloor;
        result.ShouldNotBeNull();

    }
}