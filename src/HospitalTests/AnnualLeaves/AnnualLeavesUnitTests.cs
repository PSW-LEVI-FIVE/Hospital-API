using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.AnnualLeaves;

public class AnnualLeavesUnitTests
{
    public Mock<IUnitOfWork> AnnualLeaveRepositoryMock()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var annualLeaveRepository = new Mock<IAnnualLeaveRepository>();
        unitOfWork.Setup(unit => unit.AnnualLeaveRepository).Returns(annualLeaveRepository.Object);
        return unitOfWork;
    }

    [Fact]
    public void Can_create_annual_leave_after_five_days()
    {
        AnnualLeave annualLeave =
            new AnnualLeave(1, null, "First Reason", DateTime.Now.AddDays(7), DateTime.Now.AddDays(9), AnnualLeaveState.PENDING, false);
        
        bool isValid = annualLeave.IsValid();

        isValid.ShouldBe(true);
    }

    [Fact]
    public void Can_not_create_annual_leave_under_five_days()
    {
        AnnualLeave annualLeave =
            new AnnualLeave(1, null, "Second Reason", DateTime.Now.AddDays(3), DateTime.Now.AddDays(5), AnnualLeaveState.PENDING, false);

        bool isValid = annualLeave.IsValid();

        isValid.ShouldBe(false);
    }

    [Fact]
    public void Can_not_create_annual_leave_end_before_start()
    {
        AnnualLeave annualLeave =
            new AnnualLeave(1, null, "Second Reason", DateTime.Now.AddDays(9), DateTime.Now.AddDays(7), AnnualLeaveState.PENDING, false);

        bool isValid = annualLeave.IsValid();

        isValid.ShouldBe(false);
    }

    [Fact]
    public void Can_delete_annual_leave()
    {
        var mock = AnnualLeaveRepositoryMock();
        AnnualLeave annualLeave = new AnnualLeave(1, null, "Annual Leave is PENDING",
                DateTime.Now, DateTime.Now, AnnualLeaveState.PENDING, true);
        mock.Setup(work =>
            work.AnnualLeaveRepository.GetOne(It.IsAny<int>())).Returns(annualLeave);
        AnnualLeaveService service = new AnnualLeaveService(mock.Object,null);

        AnnualLeave response=service.Delete(annualLeave.Id,1);
        
        response.ShouldNotBeNull();
    }
    
    [Fact]
    public void Can_not_delete_annual_leave()
    {
        var mock = AnnualLeaveRepositoryMock();
        AnnualLeave annualLeave = new AnnualLeave(1, null, "Annual Leave is NOT-PENDING",
            DateTime.Now, DateTime.Now, AnnualLeaveState.APPROVED, true);
        mock.Setup(work =>
            work.AnnualLeaveRepository.GetOne(It.IsAny<int>())).Returns(annualLeave);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object);
        AnnualLeaveService service = new AnnualLeaveService(mock.Object,validator);
        
        Assert.Throws<BadRequestException>(() => service.Delete(annualLeave.Id,1));
    }
    
    [Fact]
    public void Can_not_delete_annual_leave_false_doctorId()
    {
        var mock = AnnualLeaveRepositoryMock();
        AnnualLeave annualLeave = new AnnualLeave(1, null, "Annual Leave is PENDING, but false DOCTOR_ID",
            DateTime.Now, DateTime.Now, AnnualLeaveState.PENDING, true);
        mock.Setup(work =>
            work.AnnualLeaveRepository.GetOne(It.IsAny<int>())).Returns(annualLeave);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object);
        AnnualLeaveService service = new AnnualLeaveService(mock.Object,validator);

        Assert.Throws<BadRequestException>(() => service.Delete(annualLeave.Id, 2));
    }
}