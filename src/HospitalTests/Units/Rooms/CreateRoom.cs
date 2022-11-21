using HospitalLibrary.Map;
using HospitalLibrary.Map.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Rooms;

public class CreateRoom
{
    [Fact]
    public void Creates_room_successfully()
    {
        var roomRepository = new Mock<IRoomRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(u => u.RoomRepository).Returns(roomRepository.Object);
        unitOfWork.Setup(u => u.RoomRepository.Add(It.IsAny<Room>())).Verifiable();
        unitOfWork.Setup(u => u.RoomRepository.Save()).Verifiable();

        var roomService = new RoomService(unitOfWork.Object);

        Room room = new Room()
        {
            RoomNumber = "20A",
            Area = 45
        };

        var result = roomService.Create(room);
        result.ShouldNotBeNull();
    }

    [Fact]
    public void Creates_map_room_successfully()
    {
        var mapRoomRepository = new Mock<IMapRoomRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(u => u.MapRoomRepository).Returns(mapRoomRepository.Object);
        unitOfWork.Setup(u => u.MapRoomRepository.Add(It.IsAny<MapRoom>())).Verifiable();
        unitOfWork.Setup(u => u.MapRoomRepository.Save()).Verifiable();

        var mapService = new MapService(unitOfWork.Object);

        MapRoom room = new MapRoom()
        {
            RoomId = 1,
            Height = 10,
            Width = 10,
            XCoordinate = 10,
            YCoordinate = 10
        };

        var result = mapService.CreateRoom(room);
        result.ShouldNotBeNull();
    }
}