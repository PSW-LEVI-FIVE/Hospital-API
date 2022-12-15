using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Appointments;

[Collection("Test")]
public class AppointmentTestsIntranet : BaseIntegrationTest
{
    public AppointmentTestsIntranet(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    private static AppointmentController SetupController(IServiceScope scope)
    {
        return new AppointmentController(scope.ServiceProvider.GetRequiredService<IAppointmentService>(),
            scope.ServiceProvider.GetRequiredService<IEmailService>());
    }

    [Fact]
    public async Task Create_appointment_for_other_doctor()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        DateTime date = DateTime.Today.AddDays(3);
        TimeSpan timeSpanBegin = new TimeSpan(12, 00, 0);
        TimeSpan timeSpanEnd = new TimeSpan(13, 25, 0);
        DateTime start = date + timeSpanBegin;
        DateTime end = date + timeSpanEnd;
        
        CreateAppointmentDTO dto = new CreateAppointmentDTO(5, 1, 3, start, end );
        var result =((OkObjectResult) await controller.CreateForAnotherDoctor(dto)).Value as Appointment;
        
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Get_calendar_intervals_for_doctor()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        DateTime start = DateTime.Today;
        var result =((OkObjectResult)await controller.GetCalendarIntervalsForDoctorInRange(start,5)).Value as List<CalendarAppointmentsDTO>;
        
        result.ShouldNotBeNull();
    }
}