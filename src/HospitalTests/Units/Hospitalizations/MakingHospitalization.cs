using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Hospitalizations;

public class Hospitalization_Making
{

    private Mock<IUnitOfWork> SetupUOW()
    {
        var hospitalizationRepository = new Mock<IHospitalizationRepository>();
        var bedRepository = new Mock<IBedRepository>();
        var medicalRecordRepository = new Mock<IMedicalRecordRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(u => u.HospitalizationRepository).Returns(hospitalizationRepository.Object);
        unitOfWork.Setup(u => u.BedRepository).Returns(bedRepository.Object);
        unitOfWork.Setup(u => u.MedicalRecordRepository).Returns(medicalRecordRepository.Object);
        return unitOfWork;
    }

    [Fact]
    public void Hospitalization_successfully_created()
    {

        var unitOfWork = SetupUOW();

        Hospitalization hospitalizationResult = null;
        
        unitOfWork.Setup(u => u.HospitalizationRepository.Create(It.IsAny<Hospitalization>()))
            .Callback<Hospitalization>(val => hospitalizationResult = val);
        unitOfWork.Setup(u => u.BedRepository.IsBedFree(It.IsAny<int>())).Returns(true);
        unitOfWork.Setup(u => u.MedicalRecordRepository.Exists(It.IsAny<int>())).Returns(true);

        
        var hospitalizationService = new HospitalizationService(unitOfWork.Object,null, null);

        var hospObj = new Hospitalization(1, 1, 1, new DateTime(), HospitalizationState.ACTIVE);
        
        
        var result = hospitalizationService.Create(hospObj);
        
        hospitalizationResult.ShouldNotBeNull();
        result.ShouldNotBeNull();
    }

    [Fact]
    public void Hospitalization_medical_record_exists()
    {
        var unitOfWork = SetupUOW();

        unitOfWork.Setup(u => u.HospitalizationRepository.Add(It.IsAny<Hospitalization>())).Verifiable();
        unitOfWork.Setup(u => u.HospitalizationRepository.Save()).Verifiable();
        unitOfWork.Setup(u => u.BedRepository.IsBedFree(It.IsAny<int>())).Returns(true);
        unitOfWork.Setup(u => u.MedicalRecordRepository.Exists(It.IsAny<int>())).Returns(true);


        
        var hospitalizationService = new HospitalizationService(unitOfWork.Object, null, null);

        var hospObj = new Hospitalization(1, 1, 1, new DateTime(), HospitalizationState.ACTIVE);

        var result = hospitalizationService.Create(hospObj);

        result.ShouldNotBeNull();

    }
    
    [Fact]
    public void Hospitalization_medical_record_doesnt_exist()
    {
        var unitOfWork = SetupUOW();

        
        unitOfWork.Setup(u => u.HospitalizationRepository.Add(It.IsAny<Hospitalization>())).Verifiable();
        unitOfWork.Setup(u => u.HospitalizationRepository.Save()).Returns(1);
        unitOfWork.Setup(u => u.BedRepository.IsBedFree(It.IsAny<int>())).Returns(true);
        unitOfWork.Setup(u => u.MedicalRecordRepository.Exists(It.IsAny<int>())).Returns(false);

        
        var hospitalizationService = new HospitalizationService(unitOfWork.Object,  null, null);

        var hospObj = new Hospitalization(1, 1, 1, new DateTime(), HospitalizationState.ACTIVE);

        Should.Throw<BadRequestException>(() => hospitalizationService.Create(hospObj));
    }
    
    [Fact]
    public void Hospitalization_Bed_Not_Free()
    {
        var unitOfWork = SetupUOW();

        
        unitOfWork.Setup(u => u.HospitalizationRepository.Add(It.IsAny<Hospitalization>())).Verifiable();
        unitOfWork.Setup(u => u.HospitalizationRepository.Save()).Verifiable();
        unitOfWork.Setup(u => u.BedRepository.IsBedFree(It.IsAny<int>())).Returns(false);
        unitOfWork.Setup(u => u.MedicalRecordRepository.Exists(It.IsAny<int>())).Returns(true);

        
        var hospitalizationService = new HospitalizationService(unitOfWork.Object,  null, null);

        var hospObj = new Hospitalization(1, 1, 1, new DateTime(), HospitalizationState.ACTIVE);

        Should.Throw<BadRequestException>(() => hospitalizationService.Create(hospObj));
    }
    
    
}