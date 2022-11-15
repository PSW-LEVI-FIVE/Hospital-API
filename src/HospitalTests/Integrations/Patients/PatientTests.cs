using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Buildings.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations;

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
        Patient patient = new Patient("Pera", "Peric", "gmail@gmail.com","29857235","5455454",new DateTime(2000,2,25),"Mikse Dimitrijevica 42");
        var result = ((OkObjectResult)controller.Create(patient).Result).Value as Patient;
        
        result.ShouldNotBeNull();
    }
}