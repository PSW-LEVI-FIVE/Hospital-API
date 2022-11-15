using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.AnnualLeaves;

public class AnnualLeavesUnitTests
{
    public Mock<IUnitOfWork> AnnualLeaveRepositorySetup()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var annualLeaveRepository = new Mock<IAnnualLeaveRepository>();
        unitOfWork.Setup(unit => unit.AnnualLeaveRepository).Returns(annualLeaveRepository.Object);
        List<AnnualLeave> annualLeaves = new List<AnnualLeave>();
        Doctor doctor1 = new Doctor("Doctor1", "Doctor1", "Doctor1", "Doctor1",
            "5464",DateTime.Now, "asd", SpecialtyType.SURGERY);
        AnnualLeave annualLeave1 =
            new AnnualLeave(1, doctor1, "asd", DateTime.Now, DateTime.Now,
                AnnualLeaveState.PENDING, true);
        AnnualLeave annualLeave2 =
            new AnnualLeave(1, doctor1, "asd", DateTime.Now, DateTime.Now,
                AnnualLeaveState.PENDING, true);
    
        annualLeaves.Add(annualLeave1);
        annualLeaves.Add(annualLeave2);
        
        IEnumerable<AnnualLeave> annualLeavesEnumerable = annualLeaves.AsEnumerable();
        annualLeaveRepository.Setup(unit => unit.GetAll()).ReturnsAsync(annualLeavesEnumerable);
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
}