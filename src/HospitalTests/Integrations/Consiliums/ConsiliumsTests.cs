using System.Security.Claims;
using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Consiliums;
using HospitalLibrary.Consiliums.Dtos;
using HospitalLibrary.Consiliums.Interfaces;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Consiliums;

[Collection("Test")]
public class ConsiliumTests:BaseIntegrationTest
{
    public ConsiliumTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
        using var scope = Factory.Services.CreateScope();
    }
    private ConsiliumController CreateFakeControllerWithIdentity(IConsiliumService consiliumService) {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "Somebody"),
            new Claim(ClaimTypes.NameIdentifier, "6"),
            new Claim(ClaimTypes.Role, "Patient"),
        }, "mock"));
        
        var controller = new ConsiliumController(consiliumService);
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
        return controller;
    }

    [Fact]
    public async void CreateConsilium()
    {
        using var scope = Factory.Services.CreateScope();
        var consiliumController = CreateFakeControllerWithIdentity(scope.ServiceProvider.GetRequiredService<IConsiliumService>());
        CreateConsiliumDTO createConsiliumDto = new CreateConsiliumDTO
        {
            Start = DateTime.Now.AddDays(3),
            End = DateTime.Now.AddDays(8),
            Doctors = new List<int> { 4, 5 },
            RoomId = 1,
            Title = "Sometitle"
        };
        var res = ((OkObjectResult )await consiliumController.Create(createConsiliumDto)).Value
            as Consilium;
        
        res.ShouldNotBeNull();
        res.Title.TitleString.ShouldBe("Sometitle");
    }
}