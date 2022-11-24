using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Therapies.Model;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.BloodStorages;

[Collection("Test")]
public class BloodStorageTests: BaseIntegrationTest
{
    public BloodStorageTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    private static BloodStorageController SetupController(IServiceScope scope)
    {
        return new BloodStorageController(scope.ServiceProvider.GetRequiredService<IBloodStorageService>());
    }
    
    [Fact]
    public void Get_all_blood_storage()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);

        List<BloodType> result = new List<BloodType>();
        result =((OkObjectResult) controller.GetBloodStorage(10)).Value as List<BloodType>;
        
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<BloodType>>();
        result.Count.ShouldBe(2); 
    }
}