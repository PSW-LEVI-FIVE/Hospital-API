using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Allergens.Interfaces;
using HospitalLibrary.Auth;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Persons.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Interfaces;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Patients;

public class ActivateAccountTests
{
            public AuthService ActivationServiceSetup()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var personRepository = new Mock<IPersonRepository>();
            var userRepository = new Mock<IUserRepository>();

            unitOfWork.Setup(unit => unit.PersonRepository).Returns(personRepository.Object);
            unitOfWork.Setup(unit => unit.UserRepository).Returns(userRepository.Object);
            
            Patient p1 = new Patient("Pera","Peric","gmail1@gmail.com",
                                            "11111111","420420",new DateTime(2000,2,2),
                                            "Mike Mikica",BloodType.ZERO_NEGATIVE);
            
            User u1 = new User("kiki", "sifra", Role.Patient,1,ActiveStatus.Pending);
            u1.ActivationCode = "asdasd";
            u1.Person = p1;
            personRepository.Setup(unit => unit.GetOneByEmail("gmail1@gmail.com")).Returns(p1);
            personRepository.Setup(unit => unit.GetOneByUid("11111111")).Returns(p1);
            userRepository.Setup(unit => unit.GetOneByCode("asdasd")).ReturnsAsync(u1);
       
            AuthService authService = new AuthService(
                unitOfWork.Object,
                new RegistrationValidationService(unitOfWork.Object),
                new UserService(unitOfWork.Object),
                new PatientService(unitOfWork.Object), 
                null
            );
            return authService;
        }
    [Fact]
    public void Activation_failed()
    {
        Should.Throw<BadRequestException>(() => ActivationServiceSetup().
            ActivateAccount("notACode")).Message.ShouldBe("Activation code not valid!");
    }
    [Fact]
    public async void Activation_success()
    {
        var patiendDTO = await ActivationServiceSetup().ActivateAccount("asdasd");
        patiendDTO.ShouldNotBe(null);
    }
}