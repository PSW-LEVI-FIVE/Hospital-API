using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Therapies.Dtos;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Therapies.Model;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Therapies;


[Collection("Test")]
public class TherapiesTests : BaseIntegrationTest
{
    public TherapiesTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }

    public TherapyController SetupController(IServiceScope scope)
    {
        return new TherapyController(scope.ServiceProvider.GetRequiredService<ITherapyService>());
    }

    [Fact]
    public async Task Create_blood_therapy()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
    
        var dto = new CreateBloodTherapyDTO()
        {
            HospitalizationId = 10,
            GivenAt = DateTime.Now,
            Type = BloodType.A_NEGATIVE,
            Quantity = 2.0,
            DoctorId = 4
        };
        var result = ((OkObjectResult)await controller.CreateBloodTherapy(dto)).Value as BloodTherapy;
        result.ShouldNotBeNull();
        result.ShouldBeOfType<BloodTherapy>();
    }
    
    [Fact]
    public void Create_medicine_therapy()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
    
        var dto = new CreateMedicineTherapyDTO()
        {
            HospitalizationId = 10,
            GivenAt = DateTime.Now,
            MedicineId = 1,
            Quantity = 2.0,
            DoctorId = 4
        };
        var result = ((OkObjectResult)controller.CreateMedicineTherapy(dto)).Value as MedicineTherapy;
        result.ShouldNotBeNull();
        result.ShouldBeOfType<MedicineTherapy>();
    }
}