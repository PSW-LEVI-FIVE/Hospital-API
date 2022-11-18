using System.Data.SqlClient;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Allergens.Interfaces;
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
        public AuthService RegistrationServiceSetup()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            
            var personRepository = new Mock<IPersonRepository>();
            var userRepository = new Mock<IUserRepository>();
            var alergenRepository = new Mock<IAllergenRepository>();
            
            unitOfWork.Setup(unit => unit.PersonRepository).Returns(personRepository.Object);
            unitOfWork.Setup(unit => unit.UserRepository).Returns(userRepository.Object);
            unitOfWork.Setup(unit => unit.AllergenRepository).Returns(alergenRepository.Object);
            
            Patient p1 = new Patient("Pera","Peric","gmail1@gmail.com",
                                            "11111111","420420",new DateTime(2000,2,2),
                                            "Mike Mikica",BloodType.ZERO_NEGATIVE,new List<Allergen>());
            
            User u1 = new User("kiki", "sifra", Role.Patient,1);
            
            Allergen allergen1 = new Allergen(1,"Milk");
            Allergen allergen2 = new Allergen(2,"Cetirizine");
            Allergen allergen3 = new Allergen(3,"Budesonide");
            
            alergenRepository.Setup(unit => unit.GetOneByName("Milk")).ReturnsAsync(allergen1);
            alergenRepository.Setup(unit => unit.GetOneByName("Cetirizine")).ReturnsAsync(allergen2);
            alergenRepository.Setup(unit => unit.GetOneByName("Budesonide")).ReturnsAsync(allergen3);
            
            personRepository.Setup(unit => unit.GetOneByEmail("gmail1@gmail.com")).Returns(p1);
            personRepository.Setup(unit => unit.GetOneByUid("11111111")).Returns(p1);
            
            userRepository.Setup(unit => unit.GetOneByUsername("kiki")).Returns(u1);
                    
            AuthService authService = new AuthService(unitOfWork.Object,new RegistrationValidationService(unitOfWork.Object));
            return authService;
        }
        
        [Fact]
        public void Create_patient_not_unique_uid_Exception()
        {
            List<AllergenDTO> patientAllergens = new List<AllergenDTO>
            {
                new AllergenDTO("Milk"),
                new AllergenDTO("Cetirizine")
            };
            User userToCreate = new User("proxm", "sifra", Role.Patient,3);
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail2@gmail.com",
                "11111111", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE,new List<Allergen>());
            userToCreate.Person = patientToCreate;
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup().
                RegisterPatient(userToCreate,patientAllergens)).Message.ShouldBe("Uid is already taken");
        }
        [Fact]
        public void Create_patient_not_unique_email_Exception()
        {
            List<AllergenDTO> patientAllergens = new List<AllergenDTO>
            {
                new AllergenDTO("Milk"),
                new AllergenDTO("Cetirizine")
            };
            User userToCreate = new User("proxm", "sifra", Role.Patient,3);
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail1@gmail.com",
                "99999999", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE,new List<Allergen>());
            userToCreate.Person = patientToCreate;
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup().
                    RegisterPatient(userToCreate,patientAllergens)).Message.ShouldBe("Email is already taken");
        }
        [Fact]
        public void Allergen_doesnt_exist()
        {
            List<AllergenDTO> patientAllergens = new List<AllergenDTO>
            {
                new AllergenDTO("Milk"),
                new AllergenDTO("notAllergen")
            };
            User userToCreate = new User("proxm", "sifra", Role.Patient,3);
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail2@gmail.com",
                "99999999", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE,new List<Allergen>());
            userToCreate.Person = patientToCreate;
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup().
                RegisterPatient(userToCreate,patientAllergens)).Message.ShouldBe("Allergen doesnt exist!");
        }
        [Fact]
        public void Create_patient_not_unique_username_Exception()
        {
            List<AllergenDTO> patientAllergens = new List<AllergenDTO>
            {
                new AllergenDTO("Milk"),
                new AllergenDTO("Cetirizine")
            };
            User userToCreate = new User("kiki", "sifra", Role.Patient,3);
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail2@gmail.com",
                "99999999", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE,new List<Allergen>());
            userToCreate.Person = patientToCreate;
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup().
                    RegisterPatient(userToCreate,patientAllergens)).Message.ShouldBe("Username is already taken");
        }
    }
}
