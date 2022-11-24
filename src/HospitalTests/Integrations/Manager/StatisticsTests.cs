using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.MedicalRecords;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Doctors;
using Shouldly;
using HospitalLibrary.Therapies.Model;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using Moq;
using HospitalLibrary.Medicines;

namespace HospitalTests.Integrations.Manager
{
    [Collection("Test")]
    public class StatisticsTests : BaseIntegrationTest
    {
        public StatisticsTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_doctors_with_popularitiesAsync()
        {
            using var scope = Factory.Services.CreateScope();
            var emailService = new Mock<IEmailService>();
            var controller = new DoctorController(
                scope.ServiceProvider.GetRequiredService<IDoctorService>(),
                emailService.Object
            );
            List<DoctorWithPopularityDTO> result = ((OkObjectResult)await controller.GetDoctorsWithPopularity()).Value as List<DoctorWithPopularityDTO>;
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<DoctorWithPopularityDTO>>();
        }

        [Fact]
        public async Task Get_allergens_with_number_of_patientsAsync()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = new AllergenController(
                scope.ServiceProvider.GetRequiredService<IAllergenService>()
            );
            List<AllergenWithNumberPatientsDTO> result = ((OkObjectResult)await controller.GetAllergensWithNumberOfPatients()).Value as List<AllergenWithNumberPatientsDTO>;
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<AllergenWithNumberPatientsDTO>>();
        }
    }
}
