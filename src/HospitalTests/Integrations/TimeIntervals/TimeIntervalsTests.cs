using System.Security.Claims;
using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.TimeIntervals;

[Collection("Test")]
public class TimeIntervalsTests: BaseIntegrationTest
{
    public TimeIntervalsTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
        using var scope = Factory.Services.CreateScope();
    }
    private AppointmentController CreateFakeControllerWithIdentity(IDoctorService doctorService,IRoomService roomService,IAppointmentService appointmentService) {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "Somebody"),
            new Claim(ClaimTypes.NameIdentifier, "1"),
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
    public void Get_time_intervals()
    {
        using var scope = Factory.Services.CreateScope();
        var doctorService = scope.ServiceProvider.GetRequiredService<IDoctorService>();
        var roomService = scope.ServiceProvider.GetRequiredService<IRoomService>();
        var appointmentService = scope.ServiceProvider.GetRequiredService<IAppointmentService>();
        var controller = CreateFakeControllerWithIdentity(doctorService,roomService,appointmentService);
        string doctorUid = "67867867";
        var today = new DateTime();
        DateTime chosen = today.AddDays(2);
        var result = ((OkObjectResult)controller.GetTimeIntervalsForStepByStep(doctorUid,chosen).Result).Value as IEnumerable<TimeInterval>;
        result?.Count().ShouldBeEquivalentTo(18);
    }
}