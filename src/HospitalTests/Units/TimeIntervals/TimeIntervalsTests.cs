using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Shared.Validators;
using Moq;
using Shouldly;

namespace HospitalTests.Units.TimeIntervals;

public class TimeIntervalsTests
{
    public TimeIntervalValidationService TimeIntervalServiceSetup()
    {
        var appointmentRepository = new Mock<IAppointmentRepository>();
        var annualLeaveRepository = new Mock<IAnnualLeaveRepository>();
        var workingHoursRepository = new Mock<IWorkingHoursRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        
        DateTime timeBegin1 = DateTime.Today.AddDays(2);
        DateTime timeEnd1 = DateTime.Today.AddDays(2);
        TimeSpan timeSpanBegin1 = new TimeSpan(10, 0, 0);
        timeBegin1 = timeBegin1.Date + timeSpanBegin1;
        TimeSpan timeSpanEnd1 = new TimeSpan(12, 0, 0);
        timeEnd1 = timeEnd1.Date + timeSpanEnd1;
        
        DateTime timeBegin2 = DateTime.Today.AddDays(2);
        DateTime timeEnd2 = DateTime.Today.AddDays(2);
        TimeSpan timeSpanBegin2 = new TimeSpan(12, 0, 0);
        timeBegin2 = timeBegin2.Date + timeSpanBegin2;
        TimeSpan timeSpanEnd2 = new TimeSpan(18, 0, 0);
        timeEnd2 = timeEnd2.Date + timeSpanEnd2;

        List<TimeInterval> doctorIntervals = new List<TimeInterval>();
        doctorIntervals.Add(new TimeInterval(timeBegin1,timeEnd1));
        doctorIntervals.Add(new TimeInterval(timeBegin2,timeEnd2));
        List<TimeInterval> roomIntervals = new List<TimeInterval>();
        roomIntervals.Add(new TimeInterval(timeBegin1,timeEnd1));
        roomIntervals.Add(new TimeInterval(timeBegin2,timeEnd2));
        appointmentRepository.Setup(appoRepo => appoRepo
                .GetAllDoctorTakenIntervalsForDate(It.IsAny<int>(),It.IsAny<DateTime>()))
            .ReturnsAsync(doctorIntervals.AsEnumerable());
        appointmentRepository.Setup(appoRepo => appoRepo
                .GetAllRoomTakenIntervalsForDate(It.IsAny<int>(),It.IsAny<DateTime>()))
            .ReturnsAsync(roomIntervals);
        
        timeSpanBegin2 = new TimeSpan(10, 0, 0);
        timeSpanEnd2 = new TimeSpan(23, 0, 0);
        WorkingHours wh = new WorkingHours(1,1,timeSpanBegin2,timeSpanEnd2);
        workingHoursRepository.Setup(whRepo => whRepo
                .GetOne(It.IsAny<int>(),1))
            .Returns(wh);
        
        unitOfWork.Setup(u => u.AppointmentRepository).Returns(appointmentRepository.Object);
        unitOfWork.Setup(u => u.AnnualLeaveRepository).Returns(annualLeaveRepository.Object);
        unitOfWork.Setup(u => u.WorkingHoursRepository).Returns(workingHoursRepository.Object);
        return new TimeIntervalValidationService(unitOfWork.Object);
    }
    [Fact]
    public void Time_interval_in_past()
    {
        DateTime timeBegin = DateTime.Today.AddDays(-2);
        DateTime timeEnd = DateTime.Today.AddDays(-2);
        TimeSpan timeSpanBegin1 = new TimeSpan(18, 30, 0);
        timeBegin = timeBegin.Date + timeSpanBegin1;
        TimeSpan timeSpanEnd1 = new TimeSpan(19, 0, 0);
        timeEnd = timeEnd.Date + timeSpanEnd1;
        Appointment appo = new Appointment();
        appo.StartAt = timeBegin.Date + timeSpanBegin1;
        appo.EndAt = timeBegin.Date + timeSpanEnd1;
        appo.Id = 2;
        appo.DoctorId = 1;
        appo.RoomId = 2;
        Should.Throw<BadRequestException>(() => TimeIntervalServiceSetup()
            .ValidateAppointment(appo)).Message.ShouldBe("This time interval is in the past");
    }
    [Fact]
    public void Time_interval_end_before_past()
    {
        DateTime timeBegin = DateTime.Today.AddDays(2);
        DateTime timeEnd = DateTime.Today.AddDays(2);
        TimeSpan timeSpanBegin1 = new TimeSpan(19, 30, 0);
        timeBegin = timeBegin.Date + timeSpanBegin1;
        TimeSpan timeSpanEnd1 = new TimeSpan(19, 0, 0);
        timeEnd = timeEnd.Date + timeSpanEnd1;
        Appointment appo = new Appointment();
        appo.StartAt = timeBegin.Date + timeSpanBegin1;
        appo.EndAt = timeBegin.Date + timeSpanEnd1;
        appo.Id = 1;
        appo.DoctorId = 1;
        appo.RoomId = 2;
        Should.Throw<BadRequestException>(() => TimeIntervalServiceSetup()
            .ValidateAppointment(appo)).Message.ShouldBe("Start time cannot be before end time");
    }
    [Fact]
    public void Time_interval_not_in_working_hours()
    {
        DateTime timeBegin = DateTime.Today.AddDays(2);
        DateTime timeEnd = DateTime.Today.AddDays(2);
        TimeSpan timeSpanBegin1 = new TimeSpan(9, 0, 0);
        timeBegin = timeBegin.Date + timeSpanBegin1;
        TimeSpan timeSpanEnd1 = new TimeSpan(9, 30, 0);
        timeEnd = timeEnd.Date + timeSpanEnd1;
        Appointment appo = new Appointment();
        appo.StartAt = timeBegin.Date + timeSpanBegin1;
        appo.EndAt = timeBegin.Date + timeSpanEnd1;
        appo.Id = 1;
        appo.DoctorId = 1;
        appo.RoomId = 2;
        Should.Throw<BadRequestException>(() => TimeIntervalServiceSetup()
            .ValidateAppointment(appo)).Message.ShouldBe("Requested time does not follow the working hours rule");
    }
    [Fact]
    public void Time_interval_overlaping()
    {
        DateTime timeBegin = DateTime.Today.AddDays(2);
        DateTime timeEnd = DateTime.Today.AddDays(2);
        TimeSpan timeSpanBegin1 = new TimeSpan(12, 0, 0);
        timeBegin = timeBegin.Date + timeSpanBegin1;
        TimeSpan timeSpanEnd1 = new TimeSpan(14, 30, 0);
        timeEnd = timeEnd.Date + timeSpanEnd1;
        Appointment appo = new Appointment();
        appo.StartAt = timeBegin.Date + timeSpanBegin1;
        appo.EndAt = timeBegin.Date + timeSpanEnd1;
        appo.Id = 1;
        appo.DoctorId = 1;
        appo.RoomId = 2;
        Should.Throw<BadRequestException>(() => TimeIntervalServiceSetup()
            .ValidateAppointment(appo)).Message.ShouldBe("This time interval is already in use");
    }
}