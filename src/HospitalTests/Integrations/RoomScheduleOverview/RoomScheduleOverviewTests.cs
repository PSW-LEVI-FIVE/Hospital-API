using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Appointments;

using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Renovations.Interface;
using HospitalLibrary.Renovations.Model;
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


        

        var controller = new RoomsController(scope.ServiceProvider.GetRequiredService<IRoomService>(),scope.ServiceProvider.GetRequiredService<IAppointmentService>(),scope.ServiceProvider.GetRequiredService<IEquipmentReallocationService>(),scope.ServiceProvider.GetRequiredService<IRenovationService>());
        var result = ((OkObjectResult)controller.GetRoomSchedule(1).Result).Value as IEnumerable<Appointment>;

        result.ShouldNotBeEmpty();
    }
    
    [Fact]
    public void get_all_equipment_reloactions()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new RoomsController(scope.ServiceProvider.GetRequiredService<IRoomService>(),scope.ServiceProvider.GetRequiredService<IAppointmentService>(),scope.ServiceProvider.GetRequiredService<IEquipmentReallocationService>(),scope.ServiceProvider.GetRequiredService<IRenovationService>());
        var result = ((OkObjectResult)controller.GetRoomEquipmentRelocation(1).Result).Value as IEnumerable<HospitalLibrary.Rooms.Model.EquipmentReallocation>;
        result.ShouldNotBeEmpty();
    }
    
    [Fact]
    public void get_all_renovations()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new RoomsController(scope.ServiceProvider.GetRequiredService<IRoomService>(),scope.ServiceProvider.GetRequiredService<IAppointmentService>(),scope.ServiceProvider.GetRequiredService<IEquipmentReallocationService>(),scope.ServiceProvider.GetRequiredService<IRenovationService>());
        var result = ((OkObjectResult)controller.GetRoomRenovation(1).Result).Value as IEnumerable<Renovation>;
        result.ShouldNotBeEmpty();
    }

    
    
    
    
    
}