using ceTe.DynamicPDF.PageElements.BarCoding;
using HospitalAPI.Storage;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.MedicalRecords.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.PDFGeneration;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Therapies.Model;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Hospitalizations;

public class PdfGeneration
{


    private Mock<IUnitOfWork> SetupUOW()
    {
        var hospitalizationRepository = new Mock<IHospitalizationRepository>();
        var bedRepository = new Mock<IBedRepository>();
        var medicalRecordRepository = new Mock<IMedicalRecordRepository>();
        var patientRepository = new Mock<IPatientRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(u => u.HospitalizationRepository).Returns(hospitalizationRepository.Object);
        unitOfWork.Setup(u => u.BedRepository).Returns(bedRepository.Object);
        unitOfWork.Setup(u => u.MedicalRecordRepository).Returns(medicalRecordRepository.Object);
        unitOfWork.Setup(u => u.PatientRepository).Returns(patientRepository.Object);
        return unitOfWork;
    }

    private Mock<IStorage> SetupStorage()
    {
        var storage = new Mock<IStorage>();
        storage.Setup(s => s.UploadFile(It.IsAny<byte[]>(), It.IsAny<string>())).ReturnsAsync("url");
        return storage;
    }
    
    private Mock<IHospitalizationValidator> SetupValidator()
    {
        var validator = new Mock<IHospitalizationValidator>();
        return validator;
    }

    
    [Fact]
    public async Task PDF_generated_successfully()
    {
        var unitOfWork = SetupUOW();
        var storage = SetupStorage();
        var validator = SetupValidator();
        var generator = new PdfGenerator();

        var medRec = new MedicalRecord()
        {
            Id = 1,
            PatientId = 1
        };

        var therapies = new List<HospitalLibrary.Therapies.Model.Therapy>()
        {
            new BloodTherapy(1, 1, DateTime.Now, BloodType.A_NEGATIVE, 10),
            new MedicineTherapy(1, 1, DateTime.Now, 1, 10)
        };

        var patient = new Patient()
        {
            Id = 1,
            Name = "Srdjan",
            Surname = "Stjepanovic",
            Address = "Neka adresa",
            BirthDate = DateTime.Now,
            BloodType = BloodType.A_NEGATIVE,
            Email = "sasas@gmail.com",
            PhoneNumber = "213123",
            Uid = "ASDASDASDASD"
        };

        var hosp = new Hospitalization()
        {
            Id = 1,
            BedId = 1,
            EndTime = DateTime.Now,
            StartTime = DateTime.Now,
            MedicalRecordId = 1,
            MedicalRecord = medRec,
            PdfUrl = "",
            State = HospitalizationState.FINISHED,
            Therapies = therapies
        };
        

        unitOfWork.Setup(u => u.HospitalizationRepository.GetOnePopulated(It.IsAny<int>())).Returns(hosp);
        unitOfWork.Setup(u => u.PatientRepository.GetOne(It.IsAny<int>())).Returns(patient);
        
        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator.Object, storage.Object, generator);

        var result = await hospitalizationService.GenerateTherapyReport(1);

        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void Hospitalization_doesnt_exist()
    {
        var unitOfWork = SetupUOW();
        var storage = SetupStorage();
        var validator = SetupValidator();
        var generator = new PdfGenerator();

        unitOfWork.Setup(u => u.HospitalizationRepository.GetOnePopulated(It.IsAny<int>())).Returns(null as Hospitalization);
        unitOfWork.Setup(u => u.PatientRepository.GetOne(It.IsAny<int>())).Returns(null as Patient);
        
        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator.Object, storage.Object, generator);

        Should.Throw<NotFoundException>(async () => await hospitalizationService.GenerateTherapyReport(1));
    }
}