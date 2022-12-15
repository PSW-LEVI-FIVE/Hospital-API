using HospitalAPI;
using HospitalAPI.Controllers.Intranet;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Interfaces;

namespace HospitalTests.Integrations.Rooms
{
    [Collection("Test")]
    public class RoomSearchTests : BaseIntegrationTest
    {
        public RoomSearchTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public   void empty_search_field()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = new RoomsController(scope.ServiceProvider.GetRequiredService<IRoomService>(),scope.ServiceProvider.GetRequiredService<IAppointmentService>(),scope.ServiceProvider.GetRequiredService<IEquipmentReallocationService>());
            var dto = new RoomSearchDTO(RoomType.NO_TYPE, "");
            var result =  controller.SearchRooms(1,dto).Result;
            var res = ((OkObjectResult) result).Value as IEnumerable<Room>;
            res.ShouldBeEmpty();
        }
        
        [Fact]
        public   void filled_search_field()
        {
            var scope = Factory.Services.CreateScope();
            var controller = new RoomsController(scope.ServiceProvider.GetRequiredService<IRoomService>(),scope.ServiceProvider.GetRequiredService<IAppointmentService>(),scope.ServiceProvider.GetRequiredService<IEquipmentReallocationService>());
            var dto = new RoomSearchDTO(RoomType.CAFETERIA,"1");
            var res = controller.SearchRooms(1,dto).Result;
            var result = ((OkObjectResult)res).Value as Room;
            result.ShouldBeNull();
        }
    }
}
