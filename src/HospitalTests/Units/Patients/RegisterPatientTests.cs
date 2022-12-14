using System.Data.SqlClient;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using HospitalLibrary.Allergens.Interfaces;
using HospitalLibrary.Auth;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Doctors;
using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Dtos;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Persons.Interfaces;
using HospitalLibrary.Shared.Dtos;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model.ValueObjects;
using HospitalLibrary.Shared.Validators;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Interfaces;
using Microsoft.Extensions.Configuration;
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
            var doctorRepository = new Mock<IDoctorRepository>();

            unitOfWork.Setup(unit => unit.PersonRepository).Returns(personRepository.Object);
            unitOfWork.Setup(unit => unit.UserRepository).Returns(userRepository.Object);
            unitOfWork.Setup(unit => unit.AllergenRepository).Returns(alergenRepository.Object);
            unitOfWork.Setup(unit => unit.DoctorRepository).Returns(doctorRepository.Object);
            
            Patient p1 = new Patient("Pera","Peric","gmail1@gmail.com",
                                            "11111111",new PhoneNumber("420420"),new DateTime(2000,2,2),
                                            new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),BloodType.ZERO_NEGATIVE);
            
            User u1 = new User("kiki", "sifra", Role.Patient,1,ActiveStatus.Active);
            Doctor d1 = new Doctor()
            {
                Id = 5,
                Name = "Prvi plus",
                Surname = "Drugi plus",
                Address = new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),
                BirthDate = DateTime.Now,
                Email = "nekimail1@gmail.com",
                PhoneNumber = new PhoneNumber("063555333"),
                Speciality = new Speciality(1, "INTERNAL_MEDICINE"),
                SpecialityId = 1,
                Uid = "67676767",
            };
            Doctor d2 = new Doctor()
            {
                Id = 5,
                Name = "Prvi minus",
                Surname = "Drugi minus",
                Address =  new Address("Srbija", "Novi Sad", "Sase Krstica", "4"),
                BirthDate = DateTime.Now,
                Email = "nekimail2@gmail.com",
                PhoneNumber = new PhoneNumber("063555333"),
                Speciality = new Speciality(1, "INTERNAL_MEDICINE"),
                SpecialityId = 1,
                Uid = "89898989",
            };
            var doctors = new List<Doctor>();
            doctors.Add(d1);
            doctors.Add(d2);
            Allergen allergen1 = new Allergen(1,"Milk");
            Allergen allergen2 = new Allergen(2,"Cetirizine");
            Allergen allergen3 = new Allergen(3,"Budesonide");
            
            alergenRepository.Setup(unit => unit.GetOneByName("Milk")).ReturnsAsync(allergen1);
            alergenRepository.Setup(unit => unit.GetOneByName("Cetirizine")).ReturnsAsync(allergen2);
            alergenRepository.Setup(unit => unit.GetOneByName("Budesonide")).ReturnsAsync(allergen3);
            
            personRepository.Setup(unit => unit.GetOneByEmail("gmail1@gmail.com")).Returns(p1);
            personRepository.Setup(unit => unit.GetOneByUid("11111111")).Returns(p1);
            
            userRepository.Setup(unit => unit.GetOneByUsername("kiki")).Returns(u1);
            d1.Patients = new List<Patient>();
            d1.Patients.Add(p1);
            doctorRepository.Setup(unit => unit.GetMostUnburdenedDoctor()).ReturnsAsync(d1);
            doctorRepository.Setup(unit => unit.GetUnburdenedDoctors(d1.Patients.Count))
                            .ReturnsAsync(doctors.AsEnumerable());
       
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
        public void Create_patient_not_unique_uid_Exception()
        {
            List<AllergenDTO> patientAllergens = new List<AllergenDTO>
            {
                new AllergenDTO("Milk"),
                new AllergenDTO("Cetirizine")
            };
            CreatePatientDTO patientToCreate = new CreatePatientDTO("Zika", "Zikic", "gmail2@gmail.com",
                "11111111", new PhoneNumber("555555"), new DateTime(2000,2,2), 
                new AddressDTO("Jovina 12","Jovina 12","Jovina 12","Jovina 12"),
                BloodType.ZERO_NEGATIVE,"proxm","sifra",patientAllergens,"67676767");
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup().
                RegisterPatient(patientToCreate)).Message.ShouldBe("Uid is already taken");
        }
        [Fact]
        public void Create_patient_not_unique_email_Exception()
        {
            List<AllergenDTO> patientAllergens = new List<AllergenDTO>
            {
                new AllergenDTO("Milk"),
                new AllergenDTO("Cetirizine")
            };
            CreatePatientDTO patientToCreate = new CreatePatientDTO("Zika", "Zikic", "gmail1@gmail.com",
                "99999999", new PhoneNumber("555555"), new DateTime(2000,2,2), 
                new AddressDTO("Jovina 12","Jovina 12","Jovina 12","Jovina 12"),
                BloodType.ZERO_NEGATIVE,"proxm","sifra",patientAllergens,"67676767");
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup().
                    RegisterPatient(patientToCreate)).Message.ShouldBe("Email is already taken");
        }
        [Fact]
        public void Allergen_doesnt_exist()
        {
            List<AllergenDTO> patientAllergens = new List<AllergenDTO>
            {
                new AllergenDTO("Milk"),
                new AllergenDTO("notAllergen")
            };
            CreatePatientDTO patientToCreate = new CreatePatientDTO("Zika", "Zikic", "gmail2@gmail.com",
                "99999999", new PhoneNumber("555555"), new DateTime(2000,2,2), 
                new AddressDTO("Jovina 12","Jovina 12","Jovina 12","Jovina 12"),
                BloodType.ZERO_NEGATIVE,"proxm","sifra",patientAllergens,"67676767");
            
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup().
                RegisterPatient(patientToCreate)).Message.ShouldBe("Allergen doesnt exist!");
        }
        [Fact]
        public void Create_patient_not_unique_username_Exception()
        {
            List<AllergenDTO> patientAllergens = new List<AllergenDTO>
            {
                new AllergenDTO("Milk"),
                new AllergenDTO("Cetirizine")
            };
            CreatePatientDTO patientToCreate = new CreatePatientDTO("Zika", "Zikic", "gmail2@gmail.com",
                "99999999", new PhoneNumber("555555"), new DateTime(2000,2,2), 
                new AddressDTO("Jovina 12","Jovina 12","Jovina 12","Jovina 12"),
                BloodType.ZERO_NEGATIVE,"kiki","sifra",patientAllergens,"67676767");
            
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup().
                    RegisterPatient(patientToCreate)).Message.ShouldBe("Username is already taken");
        }
        [Fact]
        public void Invalid_doctor()
        {
            List<AllergenDTO> patientAllergens = new List<AllergenDTO>
            {
                new AllergenDTO("Milk"),
                new AllergenDTO("Cetirizine")
            };
            CreatePatientDTO patientToCreate = new CreatePatientDTO("Zika", "Zikic", "gmail2@gmail.com",
                "99999999", new PhoneNumber("555555"), new DateTime(2000,2,2), 
                new AddressDTO("Jovina 12","Jovina 12","Jovina 12","Jovina 12"),
                BloodType.ZERO_NEGATIVE,"proxm","sifra",patientAllergens,"88888888");
            Should.Throw<BadRequestException>(() => RegistrationServiceSetup().
                RegisterPatient(patientToCreate)).Message.ShouldBe("Doctor doesnt exist or not valid!");
        }
    }
}
