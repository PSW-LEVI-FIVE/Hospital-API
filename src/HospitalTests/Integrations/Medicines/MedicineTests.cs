using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Medicines;
using HospitalLibrary.Medicines.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Medicines;

[Collection("Test")]
public class MedicineTests: BaseIntegrationTest
{
    public MedicineTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    private static MedicineController SetupController(IServiceScope scope)
    {
        return new MedicineController(scope.ServiceProvider.GetRequiredService<IMedicineService>());
    }
    
    [Fact]
    public async Task Get_all_blood_storage()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);

        List<Medicine> result = new List<Medicine>();
        result =((OkObjectResult) await controller.GetMedicine()).Value as List<Medicine>;
        
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<Medicine>>();
        result.Count.ShouldBe(1); 
    }
}