using HospitalAPI.Controllers.Intranet;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using HospitalAPI;
using Microsoft.Extensions.DependencyInjection;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.AnnualLeaves;
using Shouldly;
using HospitalLibrary.AnnualLeaves.Dtos;
using HospitalLibrary.User.Interfaces;

namespace HospitalTests.Integrations.AnnualLeaves;

[Collection("Test")]
public class AnnualLeaveTests : BaseIntegrationTest
{


    public AnnualLeaveTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    private static AnnualLeaveController SetupController(IServiceScope scope)
    {
        return new AnnualLeaveController(scope.ServiceProvider.GetRequiredService<IAnnualLeaveService>());
    }

    [Fact]
    public void Get_all_pending_requests()
    {
        using var scope = Factory.Services.CreateScope();
        var annualLeaveController = SetupController(scope);
        var result = ((OkObjectResult)annualLeaveController.GetAllPending()).Value as IEnumerable<AnnualLeave>;

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
}
