using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;
using HospitalLibrary.AnnualLeaves.Dtos;
using HospitalLibrary.Shared.Model.ValueObjects;


namespace HospitalTests.Units.AnnualLeaves;

public class AnnualLeavesUnitTests
{
    private Mock<IUnitOfWork> AnnualLeaveRepositoryMock()
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
        AnnualLeave annualLeave = new AnnualLeave(1, 1, null, "Annual Leave is PENDING",
                DateTime.Now, DateTime.Now, AnnualLeaveState.PENDING, false);
        mock.Setup(work =>
            work.AnnualLeaveRepository.GetOne(It.IsAny<int>())).Returns(annualLeave);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object, null);
        AnnualLeaveService service = new AnnualLeaveService(mock.Object, validator);

        AnnualLeave response = service.Delete(annualLeave.Id, 1);

        response.ShouldNotBeNull();
    }

    [Fact]
    public void Can_not_delete_annual_leave()
    {
        var mock = AnnualLeaveRepositoryMock();
        AnnualLeave annualLeave = new AnnualLeave(1, 1, null, "Annual Leave is NOT-PENDING",
            DateTime.Now, DateTime.Now, AnnualLeaveState.APPROVED, false);
        
        mock.Setup(work =>
            work.AnnualLeaveRepository.GetOne(It.IsAny<int>())).Returns(annualLeave);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object, null);
        AnnualLeaveService service = new AnnualLeaveService(mock.Object,validator);
        
        Should.Throw<HospitalLibrary.Shared.Exceptions.BadRequestException>(() => service.Delete(annualLeave.Id,1));
    }

    [Fact]
    public void Can_not_delete_annual_leave_false_doctorId()
    {
        var mock = AnnualLeaveRepositoryMock();
        AnnualLeave annualLeave = new AnnualLeave(1, 1, null, "Annual Leave is PENDING, but false DOCTOR_ID",
            DateTime.Now, DateTime.Now, AnnualLeaveState.PENDING, false);
        mock.Setup(work =>
            work.AnnualLeaveRepository.GetOne(It.IsAny<int>())).Returns(annualLeave);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object, null);
        AnnualLeaveService service = new AnnualLeaveService(mock.Object, validator);

        Should.Throw<HospitalLibrary.Shared.Exceptions.BadRequestException>(() => service.Delete(annualLeave.Id, 2));
    }

    [Fact]
    public void Cant_review_annual_leave_doesnt_exist()
    {
        var mock = AnnualLeaveRepositoryMock();
        AnnualLeave annualLeave = new AnnualLeave(1, 1, null, "some reason",
            DateTime.Now, DateTime.Now, AnnualLeaveState.PENDING, false);
        mock.Setup(work =>
            work.AnnualLeaveRepository.GetOne(It.IsAny<int>())).Returns(null as AnnualLeave);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object, null);
        AnnualLeaveService service = new AnnualLeaveService(mock.Object, validator);
        var dto = new ReviewLeaveRequestDTO() { State = AnnualLeaveState.CANCELED, Reason = "some reason" };
        Assert.Throws<NotFoundException>(() => service.ReviewRequest(dto, annualLeave.Id));
    }

    [Fact]
    public void Cant_review_annual_leave_isnt_pending()
    {
        var mock = AnnualLeaveRepositoryMock();
        AnnualLeave annualLeave = new AnnualLeave(1, 1, null, "some reason",
            DateTime.Now, DateTime.Now, AnnualLeaveState.APPROVED, false);
        
        mock.Setup(work =>
            work.AnnualLeaveRepository.GetOne(It.IsAny<int>())).Returns(annualLeave);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object, null);
        AnnualLeaveService service = new AnnualLeaveService(mock.Object, validator);
        var dto = new ReviewLeaveRequestDTO() { State = AnnualLeaveState.CANCELED, Reason = "some reason"};
        Assert.Throws<HospitalLibrary.Shared.Exceptions.BadRequestException>(() => service.ReviewRequest(dto, annualLeave.Id));
    }

    [Fact]
    public void Cant_reject_request_without_reason()
    {
        var mock = AnnualLeaveRepositoryMock();
        AnnualLeave annualLeave = new AnnualLeave(1, 1, null, "some reason",
            DateTime.Now, DateTime.Now, AnnualLeaveState.PENDING, false);
        
        mock.Setup(work =>
            work.AnnualLeaveRepository.GetOne(It.IsAny<int>())).Returns(annualLeave);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object, null);
        AnnualLeaveService service = new AnnualLeaveService(mock.Object, validator);
        var dto = new ReviewLeaveRequestDTO() { State = AnnualLeaveState.CANCELED};
        Assert.Throws<Exception>(() => service.ReviewRequest(dto, annualLeave.Id));
    }

    [Fact]
    public void Request_reviewed_successfully()
    {
        var mock = AnnualLeaveRepositoryMock();
        AnnualLeave annualLeave = new AnnualLeave(1, 1, null, "some reason",
            DateTime.Now, DateTime.Now, AnnualLeaveState.PENDING, false);
        mock.Setup(work =>
            work.AnnualLeaveRepository.GetOne(It.IsAny<int>())).Returns(annualLeave);
        IAnnualLeaveValidator validator = new AnnualLeaveValidator(mock.Object, null);
        AnnualLeaveService service = new AnnualLeaveService(mock.Object, validator);
        var dto = new ReviewLeaveRequestDTO() { State = AnnualLeaveState.CANCELED, Reason = "another doctor is on leave" };
        AnnualLeave result = service.ReviewRequest(dto, annualLeave.Id);
        result.State.ShouldBe(AnnualLeaveState.CANCELED);
        result.Reason.ShouldNotBeNull();
        result.ShouldNotBeNull();
    }

}