using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.MedicalRecords;

public class CreateMedicalRecord
{
    private Mock<IUnitOfWork> SetupUOW()
    {
        var medicalRecordRepository = new Mock<IMedicalRecordRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(u => u.MedicalRecordRepository).Returns(medicalRecordRepository.Object);
        return unitOfWork;
    }


    [Fact]
    public void MedicalRecord_created_successfully()
    {
        var medicalRecordRepository = new Mock<IMedicalRecordRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(u => u.MedicalRecordRepository).Returns(medicalRecordRepository.Object);
        unitOfWork.Setup(u => u.MedicalRecordRepository.GetByPatient(It.IsAny<int>())).Returns(null as MedicalRecord);
        unitOfWork.Setup(u => u.MedicalRecordRepository.Add(It.IsAny<MedicalRecord>())).Verifiable();
        unitOfWork.Setup(u => u.MedicalRecordRepository.Save()).Verifiable();
        
        var medicalRecordService = new MedicalRecordService(unitOfWork.Object);
        var result = medicalRecordService.CreateOrGet(1);
        
        medicalRecordRepository.Verify(mrr => mrr.Add(It.IsAny<MedicalRecord>()), Times.Once);
        medicalRecordRepository.Verify(mrr => mrr.Save(), Times.Once);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(0);
    }
    
    [Fact]
    public void MedicalRecord_exists()
    {
        var medicalRecordRepository = new Mock<IMedicalRecordRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var medicalRecord = new MedicalRecord { Id = 1, PatientId = 1 };
        unitOfWork.Setup(u => u.MedicalRecordRepository).Returns(medicalRecordRepository.Object);
        unitOfWork.Setup(u => u.MedicalRecordRepository.GetByPatient(It.IsAny<int>())).Returns(medicalRecord);
        unitOfWork.Setup(u => u.MedicalRecordRepository.Add(It.IsAny<MedicalRecord>())).Verifiable();
        unitOfWork.Setup(u => u.MedicalRecordRepository.Save()).Verifiable();
        
        var medicalRecordService = new MedicalRecordService(unitOfWork.Object);
        var result = medicalRecordService.CreateOrGet(1);
        
        medicalRecordRepository.Verify(mrr => mrr.Add(It.IsAny<MedicalRecord>()), Times.Never);
        medicalRecordRepository.Verify(mrr => mrr.Save(), Times.Never);
        result.ShouldNotBeNull();
    }
}