using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using Moq;

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
            new AnnualLeave(1, doctor1, "asd", DateTime.Now, DateTime.Now, AnnualLeaveState.PENDING, true);
        AnnualLeave annualLeave2 =
            new AnnualLeave(1, doctor1, "asd", DateTime.Now, DateTime.Now, AnnualLeaveState.PENDING, true);
    
        annualLeaves.Add(annualLeave1);
        annualLeaves.Add(annualLeave2);
        
        IEnumerable<AnnualLeave> annualLeavesEnumerable = annualLeaves.AsEnumerable();
        annualLeaveRepository.Setup(unit => unit.GetAll()).ReturnsAsync(annualLeavesEnumerable);
        return unitOfWork;
    }
    
}