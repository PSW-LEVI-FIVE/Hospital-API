using System.Security.Claims;
using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Therapies.Dtos;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Therapies.Model;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Http;
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
        return CreateFakeControllerWithIdentity(scope.ServiceProvider.GetRequiredService<ITherapyService>());
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
            Type = 1,
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
    
    [Fact]
    public  void Get_blood_consumption()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);

        var result =((OkObjectResult)controller.GetBloodConsumption()).Value as List<BloodConsumptionDTO>;
        
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<BloodConsumptionDTO>>();
        result.Count.ShouldBe(4); // 3 postoje u bazi a 4 ako se svi testovi pokrenu jer se kreira jos 1
    }
    
    
    [Fact]
    public void Get_all_hospitalization_therapies()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);

        List<HospitalizationTherapiesDTO> result = new List<HospitalizationTherapiesDTO>();
        result = ((OkObjectResult)controller.GetAllHospitalizationTherapies(10)).Value as List<HospitalizationTherapiesDTO>;
        
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<HospitalizationTherapiesDTO>>();
        result.Count.ShouldBe(6);//4 ako se sam pokrece 
    }
    
    
    private TherapyController CreateFakeControllerWithIdentity(ITherapyService therapyService) {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "Somebody"),
            new Claim(ClaimTypes.NameIdentifier, "4"),
            new Claim(ClaimTypes.Role, "Doctor"),
        }, "mock"));
        
        var controller = new TherapyController(therapyService);
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
        return controller;
    }
}