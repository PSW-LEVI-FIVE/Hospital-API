using System.Security.Claims;
using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Dtos;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Therapies.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.AnnualLeaves;

[Collection("Test")]
public class AnnualLeaveTests:BaseIntegrationTest
{
    public AnnualLeaveTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    private AnnualLeaveController SetupController(IServiceScope scope)
    {
        return CreateFakeControllerWithIdentity(scope.ServiceProvider.GetRequiredService<IAnnualLeaveService>());
    }
    
    [Fact]
    public void Create_annual_leave()
    {
        using var scope = Factory.Services.CreateScope();
        var annualLeaveController = SetupController(scope);
        AnnualLeaveDto annualLeaveDto = new AnnualLeaveDto
        {
            Reason = "reason",
            StartAt = DateTime.Now.AddDays(10),
            EndAt = DateTime.Now.AddDays(15),
            IsUrgent = true
        };

        var result = (OkObjectResult)annualLeaveController.Create(annualLeaveDto).Result;
        result.StatusCode.ShouldBe(200);
    }

    [Fact]
    public void Get_all_pending_requests()
    {
        using var scope = Factory.Services.CreateScope();
        var annualLeaveController = SetupController(scope);
        var result = ((OkObjectResult)annualLeaveController.GetAllPending()).Value as IEnumerable<PendingRequestsDTO>;

        result.ShouldNotBeEmpty();

    }
    [Fact]
    public void Request_reviewed()
    {
        using var scope = Factory.Services.CreateScope();
        var annualLeaveController = SetupController(scope);
        var dto = new ReviewLeaveRequestDTO(AnnualLeaveState.CANCELED, "neki razlog");
        var result = ((OkObjectResult)annualLeaveController.ReviewRequest(dto, 15)).Value as AnnualLeave;

        result.ShouldNotBeNull();
        result.State.ShouldBe(AnnualLeaveState.CANCELED);
        result.Reason.ShouldNotBeNull();

    }
    [Fact]
    public void Get_Statistics()
    {
        using var scope = Factory.Services.CreateScope();
        var annualLeaveController = SetupController(scope);
        var doctorId = 4;
        var result = ((OkObjectResult)annualLeaveController.GetMonthlyStatisticsByDoctorId(doctorId)).Value as IEnumerable<MonthlyLeavesDTO>;

        result.ShouldNotBeEmpty();
        result.Count().ShouldBe(12);
    }

    private AnnualLeaveController CreateFakeControllerWithIdentity(IAnnualLeaveService annualLeaveService) {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "Somebody"),
            new Claim(ClaimTypes.NameIdentifier, "4"),
            new Claim(ClaimTypes.Role, "Doctor"),
        }, "mock"));
        
        var controller = new AnnualLeaveController(annualLeaveService);
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
        return controller;
    }
}