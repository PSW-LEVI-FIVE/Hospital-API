using System.Security.Claims;
using ceTe.DynamicPDF.PageElements;
using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Examination;
using HospitalLibrary.Examination.Dtos;
using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Symptoms;
using HospitalLibrary.Therapies.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Examinations;


[Collection("Test")]
public class ExaminationTests: BaseIntegrationTest
{
    public ExaminationTests(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    private ExaminationReportController CreateFakeControllerWithIdentity(IExaminationReportService examinationReportService) {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "Somebody"),
            new Claim(ClaimTypes.NameIdentifier, "4"),
            new Claim(ClaimTypes.Role, "Doctor"),
        }, "mock"));
        
        var controller = new ExaminationReportController(examinationReportService);
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
        return controller;
    }

    private ExaminationReportController SetupController(IServiceScope scope)
    {
        return CreateFakeControllerWithIdentity(scope.ServiceProvider.GetRequiredService<IExaminationReportService>());
    }
    
    [Fact]
    public void Get_by_id()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);

        var result = ((OkObjectResult)controller.GetById(10)).Value as ExaminationReport;

        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void Get_by_examination()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);

        var result = ((OkObjectResult)controller.GetByExamination(41)).Value as ExaminationReport;

        result.ShouldNotBeNull();
    }


    [Fact]
    public async Task Create_report()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);

        var prescriptions = new List<Prescription>()
        {
            new Prescription(1, "3x4")
        };

        var symptoms = new List<Symptom>()
        {
            new Symptom
            {
                Id = 10,
                Name="Cough"
            },
            new Symptom
            {
                Id = 11,
                Name="Blood"
            },
            new Symptom
            {
                Name = "Somsething really bad"
            }
        };
        
        CreateExaminationReportDTO dto = new CreateExaminationReportDTO()
        {   
            Content = "This is report",
            ExaminationId = 30,
            Prescriptions = prescriptions,
            Symptoms = symptoms,
            DoctorId = 4
        };

        var result = ((OkObjectResult) await controller.Create(dto)).Value as ExaminationReport;

        result.ShouldNotBeNull();
    }
    

}