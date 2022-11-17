using HospitalLibrary.Medicines;
using HospitalLibrary.Medicines.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Therapy
{
    
    public class GiveMedicineTests
    {
        public Mock<IUnitOfWork> MedicineRepoSetup()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var medicineRepo = new Mock<IMedicineRepository>();
            unitOfWork.Setup(unit => unit.MedicineRepository).Returns(medicineRepo.Object);
            medicineRepo.Setup(unit => unit.GetOne(1)).Returns(new Medicine(1, "Paracetamol", 5.0));//LEK
            return unitOfWork;
        }

        [Fact]
        public void DeductFromQuantityTest()
        {
            var unitOfWork = MedicineRepoSetup();
            Medicine updatedMed = null;
            unitOfWork.Setup(unit => unit.MedicineRepository.Update(It.IsAny<Medicine>()))
                .Callback<Medicine>(med => updatedMed = med);
            MedicineService medicineService = new MedicineService(unitOfWork.Object);
            
            medicineService.SubtractQuantity(1,2.0).ShouldBe(true);
            updatedMed.ShouldNotBeNull();
            updatedMed.Quantity.ShouldBe(3.0);
        }

        [Fact]
        public void DeductFromLesserQuantityTest()
        {
            var unitOfWork = MedicineRepoSetup();
            Medicine updatedMed = null;
            unitOfWork.Setup(unit => unit.MedicineRepository.Update(It.IsAny<Medicine>()))
                .Callback<Medicine>(med => updatedMed = med);
            MedicineService medicineService = new MedicineService(unitOfWork.Object);
            
            Assert.Throws<BadRequestException>(() => medicineService.SubtractQuantity(1,6.0));
            updatedMed.ShouldBeNull();
        }
    }
}