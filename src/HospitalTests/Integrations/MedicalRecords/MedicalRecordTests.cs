using System.Transactions;
using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Hospitalizations.Dtos;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.MedicalRecords;

[Collection("Test")]
public class MedicalRecordTests: BaseIntegrationTest
{

    public MedicalRecordTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public void Get_one()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new MedicalRecordController(
            scope.ServiceProvider.GetRequiredService<IMedicalRecordService>()
        );
        var result = ((OkObjectResult)controller.GetOne(2)).Value as MedicalRecord;
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void Get_one_by_uid()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new MedicalRecordController(
            scope.ServiceProvider.GetRequiredService<IMedicalRecordService>()
        );
        var result = ((OkObjectResult)controller.GetOneByUid("78787878")).Value as MedicalRecord;
        result.ShouldNotBeNull();
    }

}