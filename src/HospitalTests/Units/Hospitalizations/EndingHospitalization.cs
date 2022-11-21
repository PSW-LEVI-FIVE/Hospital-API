using ceTe.DynamicPDF.PageElements.BarCoding;
using HospitalAPI.Storage;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Hospitalizations.Dtos;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Hospitalizations;

public class EndingHospitalization
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
    public void Hospitalization_successfully_ended()
    {
        var unitOfWork = SetupUOW();
        var dbHospitalization = new Hospitalization(1, 1, 1, new DateTime(), HospitalizationState.ACTIVE);
        var validator = new HospitalizationValidator(unitOfWork.Object);        
        Hospitalization updateResult = null;
        
        
        unitOfWork
            .Setup(u => u.HospitalizationRepository.Update(It.IsAny<Hospitalization>()))
            .Callback((Hospitalization h ) => { updateResult = h; });
        unitOfWork.Setup(u => u.HospitalizationRepository.GetOne(It.IsAny<int>())).Returns(dbHospitalization);

        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator, null, null);

        var dto = new EndHospitalizationDTO() { EndTime = DateTime.Now.AddDays(1) };
        var result = hospitalizationService.EndHospitalization(1, dto);
        
        result.State.ShouldBe(HospitalizationState.FINISHED);
        result.EndTime.ShouldNotBeNull();
        Assert.False(result.StartTime.CompareTo(result.EndTime) >= 0);

    }
    
    [Fact]
    public void Hospitalization_end_time_before_start_time()
    {
        var unitOfWork = SetupUOW();
        var today = DateTime.Now;
        var dbHospitalization = new Hospitalization(1, 1, 1, today, HospitalizationState.ACTIVE);
        var validator = new HospitalizationValidator(unitOfWork.Object);        
        Hospitalization updateResult = null;
        
        unitOfWork
            .Setup(u => u.HospitalizationRepository.Update(It.IsAny<Hospitalization>()))
            .Callback((Hospitalization h ) => { updateResult = h; });
        unitOfWork.Setup(u => u.HospitalizationRepository.GetOne(It.IsAny<int>())).Returns(dbHospitalization);

        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator, null ,null);

        var dto = new EndHospitalizationDTO() { EndTime = today.AddDays(-10) };
        Should.Throw<BadRequestException>(() => hospitalizationService.EndHospitalization(1, dto));
    }

    [Fact]
    public void Hospitalization_end_date_equal_to_start_date()
    {
        var unitOfWork = SetupUOW();
        var today = DateTime.Now;
        var dbHospitalization = new Hospitalization(1, 1, 1, today, HospitalizationState.ACTIVE);
        var validator = new HospitalizationValidator(unitOfWork.Object);        
        Hospitalization updateResult = null;
        
        unitOfWork
            .Setup(u => u.HospitalizationRepository.Update(It.IsAny<Hospitalization>()))
            .Callback((Hospitalization h ) => { updateResult = h; });
        unitOfWork.Setup(u => u.HospitalizationRepository.GetOne(It.IsAny<int>())).Returns(dbHospitalization);

        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator, null, null);

        var dto = new EndHospitalizationDTO() { EndTime = today};
        Should.Throw<BadRequestException>(() => hospitalizationService.EndHospitalization(1, dto));
    }
    
    [Fact]
    public void Hospitalization_doesnt_exist()
    {
        var unitOfWork = SetupUOW();
        var today = DateTime.Now;
        var dbHospitalization = new Hospitalization(1, 1, 1, today, HospitalizationState.ACTIVE);
        var validator = new HospitalizationValidator(unitOfWork.Object);        
        Hospitalization updateResult = null;
        
        unitOfWork
            .Setup(u => u.HospitalizationRepository.Update(It.IsAny<Hospitalization>()))
            .Callback((Hospitalization h ) => { updateResult = h; });
        unitOfWork.Setup(u => u.HospitalizationRepository.GetOne(It.IsAny<int>())).Returns(null as Hospitalization);

        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator, null, null);

        var dto = new EndHospitalizationDTO() { EndTime = today};
        Should.Throw<NotFoundException>(() => hospitalizationService.EndHospitalization(1, dto));
    }

    [Fact]
    public void Hospitalization_already_ended()
    {
        var unitOfWork = SetupUOW();
        var today = DateTime.Now;
        var dbHospitalization = new Hospitalization(1, 1, 1, today, HospitalizationState.FINISHED);
        var validator = new HospitalizationValidator(unitOfWork.Object);        
        Hospitalization updateResult = null;
        
        unitOfWork
            .Setup(u => u.HospitalizationRepository.Update(It.IsAny<Hospitalization>()))
            .Callback((Hospitalization h ) => { updateResult = h; });
        unitOfWork.Setup(u => u.HospitalizationRepository.GetOne(It.IsAny<int>())).Returns(dbHospitalization);

        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator, null, null);

        var dto = new EndHospitalizationDTO() { EndTime = today};
        Should.Throw<BadRequestException>(() => hospitalizationService.EndHospitalization(1, dto));
    }


}