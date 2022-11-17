using HospitalLibrary.BloodStorages;
using HospitalLibrary.BloodStorages.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Therapy
{
    
    public class GiveBloodTests
    {
        public Mock<IUnitOfWork> BloodRepositorySetup()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var bloodStorageRepo = new Mock<IBloodStorageRepository>();
            unitOfWork.Setup(unit => unit.BloodStorageRepository).Returns(bloodStorageRepo.Object);
            bloodStorageRepo.Setup(unit => unit.GetByType(BloodType.A_NEGATIVE)).ReturnsAsync(new BloodStorage(BloodType.A_NEGATIVE,2.0));
            return unitOfWork;
        }

        [Fact]
        public async void DeductFromQuantityTest()
        {
            BloodStorageService bloodservice = new BloodStorageService(BloodRepositorySetup().Object);
            BloodStorage blood = await bloodservice.GetByType(BloodType.A_NEGATIVE);
            
            bloodservice.SubtractQuantity(blood,1.0).ShouldBe(true);
            blood.Quantity.ShouldBe(1.0);
        }

        [Fact]
        public async void DeductFromLesserQuantityTest()
        {
            BloodStorageService bloodservice = new BloodStorageService(BloodRepositorySetup().Object);
            BloodStorage blood = await bloodservice.GetByType(BloodType.A_NEGATIVE);

            Assert.Throws<BadRequestException>(() => bloodservice.SubtractQuantity(blood, 3.0));
        }
    }
}