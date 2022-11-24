using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Rooms.Interfaces;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Model;
using Shouldly;

namespace HospitalTests.Integrations.Equipment
{
    public class EquipmentTest: BaseIntegrationTest
    {
        public EquipmentTest(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }
        public RoomsController SetupController(IServiceScope scope)
        {
            return new RoomsController(scope.ServiceProvider.GetRequiredService<IRoomService>());
        }

        [Fact]
        public void Room_has_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetRoomEquipment(1).Result)?.Value as IEnumerable<RoomEquipment>;

            result.ShouldNotBeNull();

        }

        [Fact]
        public void Room_has_no_equipment()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);

            var result = ((OkObjectResult)controller.GetRoomEquipment(5).Result)?.Value as IEnumerable<RoomEquipment>;

            result.ShouldBeEmpty();

        }
    }
}
