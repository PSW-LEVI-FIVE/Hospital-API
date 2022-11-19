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
    public void Creates_Room_successfully()
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
}