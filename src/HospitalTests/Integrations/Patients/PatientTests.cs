using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Patients;

public class PatientTests: BaseIntegrationTest
{
    public PatientTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    public PatientController SetupController(IServiceScope scope)
    {
        return new PatientController(scope.ServiceProvider.GetRequiredService<IPatientService>());
    }

    [Fact]
    public void Register_patient()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);
        CreatePatientDTO createPatientDTO = new CreatePatientDTO("Pera", "Peric", "gmail@gmail.com","29857235","5455454",new DateTime(2000,2,25),"Mikse Dimitrijevica 42",BloodType.ZERO_NEGATIVE);
        var result = ((OkObjectResult)controller.Create(createPatientDTO).Result).Value as Patient;
        
        result.ShouldNotBeNull();
    }
}