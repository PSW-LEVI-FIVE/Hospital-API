using HospitalLibrary.BloodStorages;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model.ValueObjects;
using HospitalLibrary.Users;
using Moq;

namespace HospitalTests.Units.Users;

public class BlockingUnblockingTests
{
    public UserService UserServiceSetup()
    {
        var _unitOfWork = new Mock<IUnitOfWork>();
        var patientRepository = new Mock<IPatientRepository>();
        
        Patient patient1 = new Patient("Pera","Peric","gmail1@gmail.com",
            "11111111",new PhoneNumber("+12420420"),new DateTime(2000,2,2),
            new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),BloodType.ZERO_NEGATIVE);
        
        List<Patient> patients = new List<Patient>();
        
        
        
        
        
        
        var userService = new UserService(_unitOfWork.Object);
        return userService;
    }
}