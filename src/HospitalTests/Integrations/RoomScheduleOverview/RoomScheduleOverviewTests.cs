using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.RoomScheduleOverview;
[Collection("Test")]
public class RoomScheduleOverviewTests: BaseIntegrationTest
{
    public RoomScheduleOverviewTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public void get_all_appointments()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new RoomsController(scope.ServiceProvider.GetRequiredService<IRoomService>(),scope.ServiceProvider.GetRequiredService<IAppointmentService>());
        var result = ((OkObjectResult)controller.GetRoomSchedule(1).Result).Value as IEnumerable<Appointment>;
        result.ShouldNotBeEmpty();
    }
    
    
    
}