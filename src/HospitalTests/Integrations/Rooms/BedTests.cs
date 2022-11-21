using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Rooms;

[Collection("Test")]
public class BedTests: BaseIntegrationTest
{

    public BedTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }



    [Fact]
    public async Task Get_all()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new BedController(
            scope.ServiceProvider.GetRequiredService<IBedService>()
        );
        var result = ((OkObjectResult)await controller.GetAll()).Value as IEnumerable<Bed>;
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void Get_all_free_for_room()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new BedController(
            scope.ServiceProvider.GetRequiredService<IBedService>()
        );
        
        var result = ((OkObjectResult) controller.GetAllFreeForRoom(1)).Value as IEnumerable<Bed>;
        result.ShouldNotBeNull();
    }
    
}