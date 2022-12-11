using System.Security.Claims;
using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.Doctors.Dtos;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Doctors;

[Collection("Test")]
public class DoctorsTests: BaseIntegrationTest
{
    public DoctorsTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
        using var scope = Factory.Services.CreateScope();
    }
    private DoctorController CreateFakeControllerWithIdentity(IDoctorService doctorService, IEmailService emailService) {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "Somebody"),
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Role, "Patient"),
        }, "mock"));
        
        var controller = new DoctorController(doctorService,emailService);
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
        return controller;
    }
    [Fact]
    public void Get_doctor_for_step_by_step()
    {
        using var scope = Factory.Services.CreateScope();
        var doctorService = scope.ServiceProvider.GetRequiredService<IDoctorService>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        var controller = CreateFakeControllerWithIdentity(doctorService,emailService);
        var result = ((OkObjectResult)controller.GetDoctorsForStepByStep().Result).Value as IEnumerable<DoctorWithSpecialityDTO>;
        result?.Count().ShouldBeEquivalentTo(2);
    }
}