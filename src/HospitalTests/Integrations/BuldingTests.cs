using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Buildings;
using HospitalLibrary.Buildings.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations;

public class BuldingTests: BaseIntegrationTest
{
    public BuldingTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }


    public BuildingsController SetupController(IServiceScope scope)
    {
        return new BuildingsController(scope.ServiceProvider.GetRequiredService<IBuildingService>());
    }

    [Fact]
    public void Retrieve_single_building()
    {   
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);

        var result = ((OkObjectResult)controller.GetbyId(1)).Value as Building;

        result.ShouldNotBeNull();
    }
}