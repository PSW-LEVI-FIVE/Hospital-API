using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Validators;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Rooms;

public class RoomsForStepByStep
{
    public RoomService RoomServiceSetup()
    {
        var roomRepository = new Mock<IRoomRepository>();
        var appointmentRepository = new Mock<IAppointmentRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        
        Room room1 = new Room()
        {
            Id = 1,
            RoomNumber = "100A",
            RoomType = RoomType.EXAMINATION_ROOM
        };
        Room room2 = new Room()
        {
            Id = 2,
            RoomNumber = "100B",
            RoomType = RoomType.EXAMINATION_ROOM
        };
        List<Room> rooms = new List<Room>();
        rooms.Add(room1);
        rooms.Add(room2);
        
        DateTime timeBegin1 = DateTime.Today.AddDays(2);
        DateTime timeEnd1 = DateTime.Today.AddDays(2);
        TimeSpan timeSpanBegin1 = new TimeSpan(10, 0, 0);
        timeBegin1 = timeBegin1.Date + timeSpanBegin1;
        TimeSpan timeSpanEnd1 = new TimeSpan(18, 0, 0);
        timeEnd1 = timeEnd1.Date + timeSpanEnd1;
        
        DateTime timeBegin2 = DateTime.Today.AddDays(2);
        DateTime timeEnd2 = DateTime.Today.AddDays(2);
        TimeSpan timeSpanBegin2 = new TimeSpan(20, 0, 0);
        timeBegin2 = timeBegin2.Date + timeSpanBegin2;
        TimeSpan timeSpanEnd2 = new TimeSpan(23, 0, 0);
        timeEnd2 = timeEnd2.Date + timeSpanEnd2;

        List<TimeInterval> room1Intervals = new List<TimeInterval>();
        room1Intervals.Add(new TimeInterval(timeBegin1,timeEnd1));
        room1Intervals.Add(new TimeInterval(timeBegin2,timeEnd2));
        List<TimeInterval> room2Intervals = new List<TimeInterval>();
        room2Intervals.Add(new TimeInterval(timeBegin1,timeEnd1));
        room2Intervals.Add(new TimeInterval(timeBegin2,timeEnd2));
        
        unitOfWork.Setup(u => u.RoomRepository).Returns(roomRepository.Object);
        unitOfWork.Setup(u => u.AppointmentRepository).Returns(appointmentRepository.Object);
        roomRepository.Setup(roomRepo => roomRepo.GetHospitalExaminationRooms()).ReturnsAsync(rooms.AsEnumerable());
        appointmentRepository.Setup(appoRepo => appoRepo
            .GetAllRoomTakenIntervalsForDate(1,It.IsAny<DateTime>()))
            .ReturnsAsync(room1Intervals);
        appointmentRepository.Setup(appoRepo => appoRepo
                .GetAllRoomTakenIntervalsForDate(2,It.IsAny<DateTime>()))
            .ReturnsAsync(room2Intervals);
        
        var timeIntervalValidationService = new TimeIntervalValidationService(unitOfWork.Object);
        var roomService = new RoomService(unitOfWork.Object,timeIntervalValidationService);
        return roomService;
    }
    [Fact]
    public void Room_not_found()
    {
        DateTime timeBegin = DateTime.Today.AddDays(2);
        DateTime timeEnd = DateTime.Today.AddDays(2);
        TimeSpan timeSpanBegin1 = new TimeSpan(10, 35, 0);
        timeBegin = timeBegin.Date + timeSpanBegin1;
        TimeSpan timeSpanEnd1 = new TimeSpan(11, 15, 0);
        timeEnd = timeEnd.Date + timeSpanEnd1;
        RoomService roomService = RoomServiceSetup();
        Should.Throw<NotFoundException>(() => RoomServiceSetup()
            .GetFirstAvailableExaminationRoom(new TimeInterval(timeBegin.Date + timeSpanBegin1,
                timeEnd.Date + timeSpanEnd1))).Message.ShouldBe("Couldn't find single free room");
    }
    [Fact]
    public void Room_found()
    {
        DateTime timeBegin = DateTime.Today.AddDays(2);
        DateTime timeEnd = DateTime.Today.AddDays(2);
        TimeSpan timeSpanBegin1 = new TimeSpan(18, 30, 0);
        timeBegin = timeBegin.Date + timeSpanBegin1;
        TimeSpan timeSpanEnd1 = new TimeSpan(19, 0, 0);
        timeEnd = timeEnd.Date + timeSpanEnd1;
        RoomServiceSetup()
            .GetFirstAvailableExaminationRoom(new TimeInterval(timeBegin.Date + timeSpanBegin1,
                timeEnd.Date + timeSpanEnd1)).Result.ShouldNotBeNull();
    }
    
}