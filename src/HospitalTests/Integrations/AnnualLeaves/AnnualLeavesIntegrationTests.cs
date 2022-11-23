using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Hospitalizations.Dtos;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalAPI;
using Microsoft.Extensions.DependencyInjection;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.AnnualLeaves;
using Shouldly;
using HospitalLibrary.Therapies.Model;

namespace HospitalTests.Integrations.AnnualLeaves;

[Collection("Test")]
public class AnnualLeaveTests : BaseIntegrationTest
{


    public AnnualLeaveTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    private static AnnualLeaveController SetupController(IServiceScope scope)
    {
        return new AnnualLeaveController(scope.ServiceProvider.GetRequiredService<IAnnualLeaveService>());
    }

    [Fact]
    public void Get_all_pending_requests()
    {
        using var scope = Factory.Services.CreateScope();
        var annualLeaveController = SetupController(scope);
        var result = ((OkObjectResult)annualLeaveController.GetAllPending()).Value as IEnumerable<AnnualLeave>;

        result.ShouldNotBeEmpty();

    }
}
