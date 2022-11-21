using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Floors.Interfaces;
using HospitalLibrary.Map;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Rooms;

public class RoomTests: BaseIntegrationTest
{

    public RoomTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public void Creates_room()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new MapController(
            scope.ServiceProvider.GetRequiredService<IMapService>(),
            scope.ServiceProvider.GetRequiredService<IRoomService>()
            );

        var dto = new CreateRoomDto()
        {
            RoomNumber = "20A",
            Area = 45,
            Height = 20,
            Width = 20,
            MapFloorId = 1,
            XCoordinate = 10,
            YCoordinate = 10
        };

        var result = ((OkObjectResult)controller.CreateRoom(dto)).Value as MapRoom;
        result.ShouldNotBeNull();
    }
}