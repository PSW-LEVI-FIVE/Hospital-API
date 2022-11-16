using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.User.Interfaces;
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

    [Fact]
    public void Register_patient()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new PatientController(scope.ServiceProvider.GetRequiredService<IPatientService>(),
            scope.ServiceProvider.GetRequiredService<IUserService>());
        CreatePatientDTO createPatientDTO = new CreatePatientDTO("Pera", "Peric", "gmail@gmail.com","29857235",
            "5455454",new DateTime(2001,2,25),"Mikse Dimitrijevica 42",BloodType.ZERO_NEGATIVE,
            "pRoXm","radipls");
        var result = ((OkObjectResult)controller.Create(createPatientDTO).Result).Value as Patient;
        
        result.ShouldNotBeNull();
    }
}