using System.Data.SqlClient;
using HospitalLibrary.Auth;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Persons.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Validators;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Patients
{
    public class RegisterPatientTests
    {
        public AuthService RegistrationServiceSetup(string unique)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var personRepository = new Mock<IPersonRepository>();
            var patientRepository = new Mock<IPatientRepository>();
            var userRepository = new Mock<IUserRepository>();
            unitOfWork.Setup(unit => unit.PersonRepository).Returns(personRepository.Object);
            unitOfWork.Setup(unit => unit.PatientRepository).Returns(patientRepository.Object);
            unitOfWork.Setup(unit => unit.UserRepository).Returns(userRepository.Object);
            
            Patient p1 = new Patient("Pera","Peric","gmail1@gmail.com",
                                            "11111111","420420",new DateTime(2000,2,2),
                                            "Mike Mikica",BloodType.ZERO_NEGATIVE);
            switch (unique)
            {
                case "throwEmail":
                    personRepository.Setup(unit => unit.GetOneByUid("asdasd")).Returns((Patient)null);
                    personRepository.Setup(unit => unit.GetOneByEmail("gmail2@gmail.com")).Returns(p1);
                    break;
                case "throwUid":
                    personRepository.Setup(unit => unit.GetOneByUid("11111111")).Returns(p1);
                    personRepository.Setup(unit => unit.GetOneByEmail("gmail3@gmail.com")).Returns((Patient)null);
                    break;
                default:
                    personRepository.Setup(unit => unit.GetOneByUid("asdasd")).Returns((Patient)null);
                    personRepository.Setup(unit => unit.GetOneByEmail("gmail3@gmail.com")).Returns((Patient)null);
                    break;
            }
            AuthService authService = new AuthService(unitOfWork.Object,new RegistrationValidationService(unitOfWork.Object));
            return authService;
        }
        
        [Fact]
        public void Create_patient_not_unique_uid_Exception()
        {
            User userToCreate = new User("proxm", "sifra", Role.Patient,3);
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail3@gmail.com",
                "11111111", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE);
            userToCreate.Person = patientToCreate;
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup("throwUid").RegisterPatient(userToCreate))
                .Message.ShouldBe("Uid is already taken");
        }
        [Fact]
        public void Create_patient_not_unique_email_Exception()
        {
            User userToCreate = new User("proxm", "sifra", Role.Patient,3);
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail2@gmail.com",
                "99999999", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE);
            userToCreate.Person = patientToCreate;
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup("throwEmail").RegisterPatient(userToCreate))
                .Message.ShouldBe("Email is already taken");
        }
    }
}
