using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Rooms.DTOs;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Exceptions;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HospitalTests.Integrations.EquipmentReallocation
{
    [Collection("Test")]
    public class EquipmentReallocationTests : BaseIntegrationTest
    {

        public EquipmentReallocationTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        public EquipmentReallocationController SetupController(IServiceScope scope)
        {
            return new EquipmentReallocationController(scope.ServiceProvider.GetRequiredService<IEquipmentReallocationService>());
        }

        [Fact]
        public void Unsuccesfully_added_new_reallcation_date_in_past()
        {
            using var scope = Factory.Services.CreateScope();

            CreateEquipmentReallocationDTO equipmentReallocation = new CreateEquipmentReallocationDTO(1,2, DateTime.Parse("2022-11-23 10:30:00"),DateTime.Parse("2022-11-23 11:30:00"),1,10);
            var controller = SetupController(scope);

            //var result = ((OkObjectResult)controller.AddNewReallocation(equipmentReallocation).Result)?.Value as HospitalLibrary.Rooms.Model.EquipmentReallocation;
            Should.Throw<AggregateException>(() => ((OkObjectResult)controller.AddNewReallocation(equipmentReallocation).Result).Value);
            
        }
        [Fact]
        public void Unsuccesfully_added_new_reallcation_dates_reversed()
        {
            using var scope = Factory.Services.CreateScope();

            CreateEquipmentReallocationDTO equipmentReallocation = new CreateEquipmentReallocationDTO(1, 2, DateTime.Parse("2022-11-24 10:30:00"), DateTime.Parse("2022-11-23 11:30:00"), 1, 10);
            var controller = SetupController(scope);

            Should.Throw<AggregateException>(() => ((OkObjectResult)controller.AddNewReallocation(equipmentReallocation).Result).Value);
        }



    }
}
