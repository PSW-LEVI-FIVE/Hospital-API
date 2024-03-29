﻿using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Hospitalizations.Dtos;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Hospitalizations;

[Collection("Test")]
public class HospitalizationTests: BaseIntegrationTest
{

    
    public HospitalizationTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }

    [Fact]
    public void Hospitalization_created_successfully()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new HospitalizationController(
            scope.ServiceProvider.GetRequiredService<IHospitalizationService>(),
            scope.ServiceProvider.GetRequiredService<IMedicalRecordService>()
        );
        var dto = new CreateHospitalizationDTO()
        {
            BedId = 1,
            PatientId = 1,
            StartTime = DateTime.Now,
            MedicalRecordId = 0
        };
        var result = ((OkObjectResult)controller.CreateHospitalization(dto)).Value as Hospitalization;
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void Hospitalization_ended()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new HospitalizationController(
            scope.ServiceProvider.GetRequiredService<IHospitalizationService>(),
            scope.ServiceProvider.GetRequiredService<IMedicalRecordService>()
        );
        var dto = new EndHospitalizationDTO(DateTime.Now);
        var result = ((OkObjectResult)controller.EndHospitalization(dto, 10)).Value as Hospitalization;
        
        result.ShouldNotBeNull();
        result.EndTime.ShouldNotBeNull();
        result.State.ShouldBe(HospitalizationState.FINISHED);
    }


    [Fact]
    public async Task PDF_generation()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new HospitalizationController(
            scope.ServiceProvider.GetRequiredService<IHospitalizationService>(),
            scope.ServiceProvider.GetRequiredService<IMedicalRecordService>()
        );
        var result = ((OkObjectResult)await controller.GeneratePdf(10)).Value as PdfGeneratedDTO;
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Get_hospitalizations_for_patient()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = new HospitalizationController(
            scope.ServiceProvider.GetRequiredService<IHospitalizationService>(),
            scope.ServiceProvider.GetRequiredService<IMedicalRecordService>()
        );
        var result = ((OkObjectResult)await controller.GetAllForPatient(1)).Value as List<Hospitalization>;
        result.ShouldNotBeNull();
    }
}