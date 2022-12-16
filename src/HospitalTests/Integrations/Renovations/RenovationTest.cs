using HospitalTests.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Renovation.Interface;
using HospitalLibrary.Renovation.Model;
using HospitalLibrary.Rooms.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using HospitalLibrary.Rooms.DTOs;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace HospitalTests.Integrations.Renovations
{
    [Collection("Test")]

    public class RenovationTest : BaseIntegrationTest
    {
        public RenovationTest(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }
        public RenovationController SetupController(IServiceScope scope)
        {
            return new RenovationController(scope.ServiceProvider.GetRequiredService<IRenovationService>());
        }
        [Fact]
        public void Unsuccesfully_added_new_renovation_date_in_past()
        {
            using var scope = Factory.Services.CreateScope();

            MergeDTO renovation = new MergeDTO(2, 4, DateTime.Parse("2022-11-23 10:30:00"), DateTime.Parse("2022-11-23 11:30:00"));
            var controller = SetupController(scope);

            Should.Throw<AggregateException>(() => ((OkObjectResult)controller.CreateMerge(renovation).Result).Value);

        }
        [Fact]
        public void Unsuccesfully_added_new_renovation_dates_reversed()
        {
            using var scope = Factory.Services.CreateScope();

            MergeDTO renovation = new MergeDTO(1, 2, DateTime.Parse("2022-11-24 10:30:00"), DateTime.Parse("2022-11-23 11:30:00"));
            var controller = SetupController(scope);

            Should.Throw<AggregateException>(() => ((OkObjectResult)controller.CreateMerge(renovation).Result).Value);
        }
    }
}
