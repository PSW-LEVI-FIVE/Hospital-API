using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.BloodOrders;
using HospitalLibrary.BloodOrders.Dtos;
using HospitalLibrary.BloodOrders.Interfaces;
using HospitalLibrary.BloodStorages;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.BloodOrders;

[Collection("Test")]
public class BloodOrderTests:BaseIntegrationTest
{
    
    public BloodOrderTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }

    private static BloodOrderController SetupController(IServiceScope scope)
    {
        return new BloodOrderController(scope.ServiceProvider.GetRequiredService<IBloodOrderService>());
    }


    [Fact]
    public void Create_blood_order()
    {
        using var scope = Factory.Services.CreateScope();
        var bloodOrderController = SetupController(scope);
        CreateBloodOrderDto bloodOrder = new CreateBloodOrderDto(4,
            DateTime.Today.AddDays(10),BloodType.A_NEGATIVE,"Need for OP",11);

        var result = ((OkObjectResult)bloodOrderController.Create(bloodOrder).Result)?.Value as BloodOrder;

        result.ShouldNotBeNull();
    }


}