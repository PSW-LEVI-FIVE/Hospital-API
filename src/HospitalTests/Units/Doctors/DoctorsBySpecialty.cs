using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model.ValueObjects;
using Moq;
using Shouldly;
using Supabase.Storage;

namespace HospitalTests.Units.Doctors;

public class DoctorsBySpecialty
{
    private Mock<IUnitOfWork> UnitOfWorkSetup()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var doctorRepository = new Mock<IDoctorRepository>();
        PhoneNumber number = new PhoneNumber("55553333");
        Doctor doc = new Doctor("Marko", "Markic", "email", "123456", number,
            new DateTime(), new Address("string", "string", "string", "string"), new Speciality(0, "UROLOGY"));
        IEnumerable<Doctor> docs = new List<Doctor>();
        docs.Append(doc);
        unitOfWork.Setup(unit => unit.DoctorRepository).Returns(doctorRepository.Object);
        unitOfWork.Setup(unit => unit.DoctorRepository.GetSpecializationById(0)).Returns(new Speciality(0, "UROLOGY"));
        unitOfWork.Setup(unit => unit.DoctorRepository.GetAllDoctorsBySpecialization(new Speciality(0, "UROLOGY"))).ReturnsAsync(docs);
        return unitOfWork;
    }
    
    
    [Fact]
    public async Task Get_Doctors_By_Specialization()
    {
        DoctorService doctorService = new DoctorService(UnitOfWorkSetup().Object);

        IEnumerable<Doctor> doctors = await doctorService.GetAllDoctorsBySpecialization(0);
        
        doctors.ShouldNotBeNull();
    }
}