using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.AnnualLeaves;

public class AnnualLeaveValidatorTests
{
    private Mock<IUnitOfWork> UnitOfWorkSetup()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var appointmentRepository = new Mock<IAppointmentRepository>();
        unitOfWork.Setup(unit => unit.AppointmentRepository).Returns(appointmentRepository.Object);
        
        return unitOfWork;
    }
    
    [Fact]
    public void Cant_create_if_doctor_not_available()
    {
        var mock = UnitOfWorkSetup();
        mock
            .Setup(work =>
            work.AppointmentRepository.GetNumberOfDoctorAppointmentsForRange(It.IsAny<int>(), It.IsAny<TimeInterval>()))
            .Returns(5);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object);
        AnnualLeave annualLeave =
            new AnnualLeave(1, null, "First Reason", DateTime.Now.AddDays(7),
                DateTime.Now.AddDays(9), AnnualLeaveState.PENDING, false);

        Assert.Throws<BadRequestException>(() => validator.Validate(annualLeave));
    }
    
    [Fact]
    public void Create_annual_leave_when_doctor_available()
    {
        var mock = UnitOfWorkSetup();
        mock
            .Setup(work =>
                work.AppointmentRepository.GetNumberOfDoctorAppointmentsForRange(It.IsAny<int>(), It.IsAny<TimeInterval>()))
            .Returns(0);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object);
        AnnualLeave annualLeave =
            new AnnualLeave(1, null, "First Reason", DateTime.Now.AddDays(7),
                DateTime.Now.AddDays(9), AnnualLeaveState.PENDING, false);

        var exception = Record.Exception(() => validator.Validate((annualLeave)));
        exception.ShouldBeNull();
    }
}