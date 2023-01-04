using HospitalAPI;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Renovations;
using HospitalLibrary.Renovations.Interface;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Validators;
using HospitalTests.Setup;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Renovation;

public class RenovationUnitTest
{
    
    
    private Mock<IUnitOfWork> RenovationRepositoryMock()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var renovationRepository = new Mock<IRenovationRepository>();
        unitOfWork.Setup(unit => unit.RenovationRepository).Returns(renovationRepository.Object);
        return unitOfWork;
    }
    [Fact]
    public void cancel_renovation()
    {
        var unitOfWork = RenovationRepositoryMock();
        var time = DateTime.Now;
            
        
        HospitalLibrary.Renovations.Model.Renovation renovation =
            new HospitalLibrary.Renovations.Model.Renovation(1,1,2,
                DateTime.Parse("2022-12-15 10:30:00"), DateTime.Parse("2022-12-17 10:30:00"));
        IRenovationValidator renovationValidator = new RenovationValidator();
        IRoomEquipmentValidator roomEquipmentValidator = new RoomEquipmentValidator(unitOfWork.Object);
        ITimeIntervalValidationService timeIntervalValidationService = new TimeIntervalValidationService(unitOfWork.Object);
        IRoomEquipmentService roomEquipmentService = new RoomEquipmentService(unitOfWork.Object,roomEquipmentValidator);
        IRoomService roomService = new RoomService(unitOfWork.Object,timeIntervalValidationService);
        RenovationService service = new RenovationService(unitOfWork.Object, timeIntervalValidationService,
            roomEquipmentService,roomService,renovationValidator
            );
        unitOfWork.Setup(work =>
            work.RenovationRepository.GetOne(It.IsAny<int>())).Returns(renovation);
        Should.Throw<HospitalLibrary.Shared.Exceptions.BadRequestException>(() =>
            service.CancelRenovation(renovation.Id));
    }
}