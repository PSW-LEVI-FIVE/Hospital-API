using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Consiliums;
using HospitalLibrary.Consiliums.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Consiliums;

public class SuggestConsiliumsUnitTests
{
    private static DateTime Date1 = DateTime.Now.AddDays(1);
    private static DateTime Date0 = DateTime.Now;
    private static DateTime Date2 = DateTime.Now.AddDays(2);
    private static DateTime Date3 = DateTime.Now.AddDays(3);
    
    private Mock<IUnitOfWork> ConsiliumMock()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var consiliumRepository = new Mock<IConsiliumRepository>();
        unitOfWork.Setup(unit => unit.ConsiliumRepository).Returns(consiliumRepository.Object);
        return unitOfWork;
    }

    
    [Fact]
    public async Task In_first_day_only_one_doctor_free()
    {
        var uowMock = ConsiliumMock();
        var validationMock = new Mock<ITimeIntervalValidationService>();
        var roomMock = new Mock<IRoomService>();
        roomMock.Setup(a => a.GetFirstAvailableConsiliumRoom(It.IsAny<TimeInterval>())).ReturnsAsync(GetRoom());
        Doctor doctor1 = GetDoctor1();
        Doctor doctor2 = GetDoctor2();
        Doctor doctor3 = GetDoctor3();
        doctor1.Appointments = new List<Appointment>();
        doctor2.Appointments = new List<Appointment>()
        {
            new Appointment()
            {
                StartAt = Date0,
                EndAt = Date2
            }
        };
        doctor3.Appointments = new List<Appointment>()
        {
            new Appointment()
            {
                StartAt = Date0,
                EndAt = Date2
            }
        };
        uowMock.Setup(u => u.DoctorRepository.GetDoctorsForDate(It.IsAny<List<int>>(), It.IsAny<DateTime>()))
            .Returns(new List<Doctor>{doctor1, doctor2, doctor3});
        var service = new ConsiliumService(uowMock.Object, validationMock.Object, roomMock.Object);

        var res = service
            .SuggestConsilium(new TimeInterval(Date1, Date3.AddDays(1)),
                new List<int> { 1, 2, 3 }, 1, 30);
        
        res.Doctors.Count.ShouldBe(3);
        res.From.Date.Day.ShouldBe(Date2.Date.Day);
    }
    
    [Fact]
    public async Task One_doctor_is_not_available_at_all2()
    {
        var uowMock = ConsiliumMock();
        var validationMock = new Mock<ITimeIntervalValidationService>();
        var roomMock = new Mock<IRoomService>();
        roomMock.Setup(a => a.GetFirstAvailableConsiliumRoom(It.IsAny<TimeInterval>())).ReturnsAsync(GetRoom());
        Doctor doctor1 = GetDoctor1();
        Doctor doctor2 = GetDoctor2();
        Doctor doctor3 = GetDoctor3();
        doctor1.Appointments = new List<Appointment>();
        doctor2.Appointments = new List<Appointment>()
        {
            new Appointment()
            {
                StartAt = Date0,
                EndAt = Date2
            }
        };
        doctor3.Appointments = new List<Appointment>()
        {
            new Appointment()
            {
                StartAt = Date0,
                EndAt = Date2.AddDays(3)
            }
        };
        uowMock.Setup(u => u.DoctorRepository.GetDoctorsForDate(It.IsAny<List<int>>(), It.IsAny<DateTime>()))
            .Returns(new List<Doctor>{doctor1, doctor2, doctor3});
        var service = new ConsiliumService(uowMock.Object, validationMock.Object, roomMock.Object);

        var res = service
            .SuggestConsilium(new TimeInterval(Date1, Date3.AddDays(1)),
                new List<int> { 1, 2, 3 }, 1, 30);
        
        res.Doctors.Count.ShouldBe(2);
        res.From.Date.Day.ShouldBe(Date2.Date.Day);
    }

    private List<Doctor> GetAllDoctors()
    {
        return new List<Doctor>
        {
            GetDoctor1(),
            GetDoctor2(),
            GetDoctor3()
        };
    }
    
    private Doctor GetDoctor3()
    {
        return new Doctor
        {
            Id = 3,
            WorkingHours = GetWorkingHours()
        };
    }

    
    private Doctor GetDoctor2()
    {
        return new Doctor
        {
            Id = 2,
            WorkingHours = GetWorkingHours()
        };
    }

    
    private Doctor GetDoctor1()
    {
        return new Doctor
        {
            Id = 1,
            WorkingHours = GetWorkingHours()
        };
    }

    private List<WorkingHours> GetWorkingHours()
    {
        WorkingHours doctor1Wh1 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 0,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh2 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 1,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh3 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 2,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh4 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 3,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh5 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 4,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh6 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 5,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };
        WorkingHours doctor1Wh7 = new WorkingHours()
        {
            DoctorId = 5,
            Day = 6,
            Start = new TimeSpan(0, 10, 30, 0),
            End = new TimeSpan(0, 23, 0, 0),
        };

        return new List<WorkingHours>()
            { doctor1Wh1, doctor1Wh2, doctor1Wh3, doctor1Wh4, doctor1Wh5, doctor1Wh6, doctor1Wh7 };
    }

    private Room GetRoom()
    {
        return new Room(1);

    }
}