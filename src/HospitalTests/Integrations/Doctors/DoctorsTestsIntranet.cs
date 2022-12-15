using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Medicines;
using HospitalLibrary.Shared.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HospitalTests.Integrations.Doctors;


[Collection("Test")]
public class DoctorsTestsIntranet:BaseIntegrationTest
{
    public DoctorsTestsIntranet(TestDatabaseFactory<Startup> factory) : base(factory)
    {
    }
    
    private static DoctorController SetupController(IServiceScope scope)
    {
        return new DoctorController(scope.ServiceProvider.GetRequiredService<IDoctorService>(),
            scope.ServiceProvider.GetRequiredService<IEmailService>());
    }
    
    [Fact]
    public async Task Get_doctors_by_specialty()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = SetupController(scope);

        IEnumerable<Doctor> result = new List<Doctor>();
        result =((OkObjectResult) await controller.GetAllDoctorsBySpecialty(2)).Value as List<Doctor>;
        
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<Doctor>>();
    }
}