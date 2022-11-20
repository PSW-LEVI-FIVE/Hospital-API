using HospitalAPI;
using HospitalAPI.Controllers.Public;
using HospitalLibrary.Auth.Interfaces;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Shared.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace HospitalTests.Integrations.Patients;

[Collection("Test")]
public class ActivateAccountTests: BaseIntegrationTest
{
    public ActivateAccountTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
        using var scope = Factory.Services.CreateScope();
    }
    [Fact]
    public void Activate_account_success()
    {
        using var scope = Factory.Services.CreateScope();
        var emailService = new Mock<IEmailService>();
        var controller = new AuthController(scope.ServiceProvider.GetRequiredService<IAuthService>(),emailService.Object);
        string code = "";
        var result = ((OkObjectResult)controller.ActivateAccount(code).Result).Value as PatientDTO;
        result.ShouldNotBeNull();
    }
}