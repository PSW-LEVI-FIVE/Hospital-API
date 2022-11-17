using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
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
        var controller = new RoomsController(
            scope.ServiceProvider.GetRequiredService<IRoomService>()
        );

        var dto = new CreateRoomDto()
        {
            RoomNumber = "20A",
            Area = 45
        };

        var result = ((OkObjectResult)controller.Create(dto)).Value as Room;
        result.ShouldNotBeNull();
    }
}