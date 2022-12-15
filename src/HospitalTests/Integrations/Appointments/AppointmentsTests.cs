using System.Security.Claims;
using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Appointments;

[Collection("Test")]
public class AppointmentsTests: BaseIntegrationTest
{
    public AppointmentsTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
        using var scope = Factory.Services.CreateScope();
    }
    private AppointmentController CreateFakeControllerWithIdentity(IDoctorService doctorService,IRoomService roomService,IAppointmentService appointmentService) {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "Somebody"),
            new Claim(ClaimTypes.NameIdentifier, "6"),
            new Claim(ClaimTypes.Role, "Patient"),
        }, "mock"));
        
        var controller = new AppointmentController(doctorService,roomService,appointmentService);
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
        return controller;
    }
    
    [Fact]
    public void Create_appointment()
    {
        using var scope = Factory.Services.CreateScope();
        var doctorService = scope.ServiceProvider.GetRequiredService<IDoctorService>();
        var roomService = scope.ServiceProvider.GetRequiredService<IRoomService>();
        var appointmentService = scope.ServiceProvider.GetRequiredService<IAppointmentService>();
        var controller = CreateFakeControllerWithIdentity(doctorService,roomService,appointmentService);
        string doctorUid = "67867867";
        DateTime date = DateTime.Today.AddDays(2);
        TimeInterval chosenTimeInterval = new TimeInterval();
        TimeSpan timeSpanBegin = new TimeSpan(11, 35, 0);
        TimeSpan timeSpanEnd = new TimeSpan(12, 25, 0);
        chosenTimeInterval.Start = date.Date + timeSpanBegin;
        chosenTimeInterval.End = date.Date + timeSpanEnd;
        var result = ((OkObjectResult)controller.Create(new CreateAppointmentForPatientDTO(doctorUid,chosenTimeInterval)).Result).Value as Appointment;
        result.ShouldNotBeNull();
    }
}