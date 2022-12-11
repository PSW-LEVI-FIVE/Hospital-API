using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTests.Units.RoomSearch
{
    public  class GoodRoomSearch
    {

        private Mock<IUnitOfWork> SetupUnitOfWork()
        {
            var roomRepository = new Mock<IRoomRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u=> u.RoomRepository).Returns(roomRepository.Object);
            List<Room> rooms = new List<Room>();
            Room r1 = new Room(1,"1",50,1,RoomType.CAFETERIA);
            Room r2  = new Room(1, "2", 40, 1, RoomType.HOSPITAL_ROOM);
            rooms.Add(r1);
            rooms.Add(r2);
            IEnumerable<Room> roomsEnumerable = rooms.AsEnumerable();
            roomRepository.Setup(r => r.GetAll()).ReturnsAsync(roomsEnumerable);
            return unitOfWork;
        }

        [Fact]
         public void room_name_typed()
        {
            var mock = SetupUnitOfWork();
            var dto = new RoomSearchDTO(RoomType.NO_TYPE, "1");
            var timeIntervalValidationService = new Mock<ITimeIntervalValidationService>();
            var roomService = new RoomService(mock.Object,timeIntervalValidationService.Object);
            var result = roomService.SearchRoom(dto,1);
            result.ShouldNotBeNull();
        }
        [Fact]
        public void room_type_typed()
        {
            var mock = SetupUnitOfWork();
            var dto = new RoomSearchDTO(RoomType.CAFETERIA, "");
            var timeIntervalValidationService = new Mock<ITimeIntervalValidationService>();
            var roomService = new RoomService(mock.Object,timeIntervalValidationService.Object);
            var result = roomService.SearchRoom(dto,1);
            result.ShouldNotBeNull();

        }
        [Fact]
        public void room_type_and_room_name_typed()
        {
            var mock = SetupUnitOfWork();
            var dto = new RoomSearchDTO(RoomType.CAFETERIA, "1");
            var timeIntervalValidationService = new Mock<ITimeIntervalValidationService>();
            var roomService = new RoomService(mock.Object,timeIntervalValidationService.Object);
            var result = roomService.SearchRoom(dto,1);
            result.ShouldNotBeNull();
        }
    }
}
