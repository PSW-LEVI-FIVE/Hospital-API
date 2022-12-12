using HospitalAPI.Storage;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Hospitalizations.Interfaces;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.Patients;
using HospitalLibrary.PDFGeneration;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model.ValueObjects;
using HospitalLibrary.Therapies.Model;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Hospitalizations;

public class PdfGeneration
{


    private Mock<IUnitOfWork> SetupUOW()
    {
        var hospitalizationRepository = new Mock<IHospitalizationRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(u => u.HospitalizationRepository).Returns(hospitalizationRepository.Object);
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

        
        var patient = new Patient()
        {
            Id = 1,
            Name = "Srdjan",
            Surname = "Stjepanovic",
            Address = new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),
            BirthDate = DateTime.Now,
            BloodType = BloodType.A_NEGATIVE,
            Email = "sasas@gmail.com",
            PhoneNumber = "213123",
            Uid = "ASDASDASDASD"
        };
        
        var medRec = new MedicalRecord()
        {
            Id = 1,
            PatientId = 1,
            Patient = patient
        };

        var therapies = new List<HospitalLibrary.Therapies.Model.Therapy>()
        {
            new BloodTherapy(1,  DateTime.Now, BloodType.A_NEGATIVE, 10, 4),
            new MedicineTherapy(1,  DateTime.Now, 1, 10, 4)
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
        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator.Object, storage.Object, generator);

        var result = await hospitalizationService.GenerateTherapyReport(1);

        result.ShouldNotBeNull();
    }
    
    [Fact]
    public void Hospitalization_doesnt_exist()
    {
        var unitOfWork = SetupUOW();
        var storage = SetupStorage();
        var validator = new HospitalizationValidator(unitOfWork.Object);
        var generator = new PdfGenerator();
        
        unitOfWork.Setup(u => u.HospitalizationRepository.GetOnePopulated(It.IsAny<int>())).Returns(null as Hospitalization);
        
        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator, storage.Object, generator);

        Should.Throw<NotFoundException>(async () => await hospitalizationService.GenerateTherapyReport(1));
    }
    
    [Fact]
    public void Hospitalization_not_finished()
    {
        var unitOfWork = SetupUOW();
        var storage = SetupStorage();
        var validator = new HospitalizationValidator(unitOfWork.Object);
        var generator = new PdfGenerator();

        
        var patient = new Patient()
        {
            Id = 1,
            Name = "Srdjan",
            Surname = "Stjepanovic",
            Address =  new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),
            BirthDate = DateTime.Now,
            BloodType = BloodType.A_NEGATIVE,
            Email = "sasas@gmail.com",
            PhoneNumber = "213123",
            Uid = "ASDASDASDASD"
        };
        
        var medRec = new MedicalRecord()
        {
            Id = 1,
            PatientId = 1,
            Patient = patient
        };

        var therapies = new List<HospitalLibrary.Therapies.Model.Therapy>()
        {
            new BloodTherapy(1,  DateTime.Now, BloodType.A_NEGATIVE, 10, 4),
            new MedicineTherapy(1,  DateTime.Now, 1, 10, 4)
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
            State = HospitalizationState.ACTIVE,
            Therapies = therapies
        };
        

        unitOfWork.Setup(u => u.HospitalizationRepository.GetOnePopulated(It.IsAny<int>())).Returns(hosp);
        
        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator, storage.Object, generator);

        Should.Throw<HospitalLibrary.Shared.Exceptions.BadRequestException>(async () => await hospitalizationService.GenerateTherapyReport(1));
    }
    
    [Fact]
    public void PDF_already_generated()
    {
        var unitOfWork = SetupUOW();
        var storage = SetupStorage();
        var validator = new HospitalizationValidator(unitOfWork.Object);
        var generator = new PdfGenerator();

        
        var patient = new Patient()
        {
            Id = 1,
            Name = "Srdjan",
            Surname = "Stjepanovic",
            Address =  new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),
            BirthDate = DateTime.Now,
            BloodType = BloodType.A_NEGATIVE,
            Email = "sasas@gmail.com",
            PhoneNumber = "213123",
            Uid = "ASDASDASDASD"
        };
        
        var medRec = new MedicalRecord()
        {
            Id = 1,
            PatientId = 1,
            Patient = patient
        };

        var therapies = new List<HospitalLibrary.Therapies.Model.Therapy>()
        {
            new BloodTherapy(1,  DateTime.Now, BloodType.A_NEGATIVE, 10, 4),
            new MedicineTherapy(1,  DateTime.Now, 1, 10, 4)
        };



        var hosp = new Hospitalization()
        {
            Id = 1,
            BedId = 1,
            EndTime = DateTime.Now,
            StartTime = DateTime.Now,
            MedicalRecordId = 1,
            MedicalRecord = medRec,
            PdfUrl = "asadasda",
            State = HospitalizationState.FINISHED,
            Therapies = therapies
        };
        

        unitOfWork.Setup(u => u.HospitalizationRepository.GetOnePopulated(It.IsAny<int>())).Returns(hosp);
        
        var hospitalizationService = new HospitalizationService(unitOfWork.Object, validator, storage.Object, generator);

        Should.Throw<HospitalLibrary.Shared.Exceptions.BadRequestException>(async () => await hospitalizationService.GenerateTherapyReport(1));
    }
}