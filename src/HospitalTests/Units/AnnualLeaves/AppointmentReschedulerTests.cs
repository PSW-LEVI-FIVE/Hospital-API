using System.Globalization;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;
using BadRequestException = HospitalLibrary.Shared.Exceptions.BadRequestException;

namespace HospitalTests.Units.AnnualLeaves;

public class AppointmentReschedulerTests
{
    private Mock<IUnitOfWork> UnitOfWorkSetup()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var appointmentRepository = new Mock<IAppointmentRepository>();
        var doctorRepository = new Mock<IDoctorRepository>();
        unitOfWork.Setup(unit => unit.AppointmentRepository).Returns(appointmentRepository.Object);
        unitOfWork.Setup(unit => unit.DoctorRepository).Returns(doctorRepository.Object);
        
        List<Doctor> doctors = new List<Doctor>();
        Doctor otherDoctor = new Doctor();
        otherDoctor.Id = 1;
        Doctor sessionDoctor = new Doctor();
        sessionDoctor.Id = 2;
        doctors.Add(otherDoctor);
        unitOfWork.Setup(work => work.DoctorRepository.GetOne(It.IsAny<int>())).Returns(sessionDoctor);
        unitOfWork.Setup(work => work.DoctorRepository
                .GetAllDoctorsWithSpecialityExceptId(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(doctors);

        Appointment appointment1 = GetAppointment(1, 2, "11-16-2022 10:30", "11-16-2022 11:30");
        Appointment appointment2 = GetAppointment(2, 2, "11-17-2022 11:30", "11-17-2022 12:30");
        
        List<Appointment> appointments = new List<Appointment>();
        appointments.Add(appointment1);
        appointments.Add(appointment2);
        unitOfWork.Setup(work =>
                work.AppointmentRepository.GetAllDoctorAppointmentsForRange(sessionDoctor.Id, It.IsAny<TimeInterval>()))
            .ReturnsAsync(appointments);
        
        return unitOfWork;
    }
    
    [Fact]
    public void Can_reschedule_appointments()
    {
        var mock = UnitOfWorkSetup();

        List<Appointment> appointments = new List<Appointment>
        {
            GetAppointment(3,1,"11-16-2022 09:30", "11-16-2022 10:00"),
            GetAppointment(4,1,"11-17-2022 13:30", "11-17-2022 14:30")
        };

        mock.Setup(work => work.AppointmentRepository
            .GetAllDoctorAppointmentsForRange(1, It.IsAny<TimeInterval>())).ReturnsAsync(appointments);
        mock.Setup(unit =>
                unit.AnnualLeaveRepository.GetDoctorsThatHaveAnnualLeaveInRange(It.IsAny<TimeInterval>()))
            .Returns(new List<int>());
        
        IAppointmentRescheduler rescheduler = new AppointmentRescheduler(mock.Object, new Mock<IEmailService>().Object);
        
        var args = new List<Appointment>();
        mock.Setup(w => w.AppointmentRepository.Update(Capture.In(args)));
        
        rescheduler.Reschedule(2, new TimeInterval(DateTime.Parse("11-13-2022"), DateTime.Parse("11-25-2022")));
        
        args.Count.ShouldBe(2);
        args.Find(a => a.Id == 1 && a.DoctorId == 1).ShouldNotBeNull();
        args.Find(a => a.Id == 2 && a.DoctorId == 1).ShouldNotBeNull();
    }
    
    [Fact]
    public void Cant_reschedule_appointments()
    {
        var mock = UnitOfWorkSetup();
        mock.Setup(unit =>
                unit.AnnualLeaveRepository.GetDoctorsThatHaveAnnualLeaveInRange(It.IsAny<TimeInterval>()))
            .Returns(new List<int>());
        List<Appointment> appointments = new List<Appointment>
        {
            GetAppointment(3,1,"11-16-2022 09:30", "11-16-2022 10:45"),
            GetAppointment(4,1,"11-17-2022 13:30", "11-17-2022 14:30")
        };

        var args = new List<Appointment>();
        mock.Setup(w => w.AppointmentRepository.Update(Capture.In(args)));
        
        mock.Setup(work => work.AppointmentRepository
            .GetAllDoctorAppointmentsForRange(1, It.IsAny<TimeInterval>())).ReturnsAsync(appointments);

        IAppointmentRescheduler rescheduler = new AppointmentRescheduler(mock.Object, new Mock<IEmailService>().Object);
        
        rescheduler.Reschedule(2, new TimeInterval(DateTime.Parse("11-13-2022"), DateTime.Parse("11-25-2022")));
        
        args.Count.ShouldBe(2);
        args.Find(a => a.Id == 1 && a.DoctorId == 2 && a.State == AppointmentState.DELETED).ShouldNotBeNull();
        args.Find(a => a.Id == 2 && a.DoctorId == 1).ShouldNotBeNull();
        
    }

    private Appointment GetAppointment(int id, int doctorId, string start, string end)
    {
        return new Appointment
        {
            Id = id,
            DoctorId = doctorId,
            StartAt = DateTime.Parse(start),
            EndAt = DateTime.Parse(end)
        };
    }
}