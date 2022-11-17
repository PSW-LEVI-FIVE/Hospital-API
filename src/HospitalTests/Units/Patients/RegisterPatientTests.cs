using System.Data.SqlClient;
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
        public PatientService PatientServiceSetup(string unique)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var personRepository = new Mock<IPersonRepository>();
            var patientRepository = new Mock<IPatientRepository>();
            unitOfWork.Setup(unit => unit.PersonRepository).Returns(personRepository.Object);
            unitOfWork.Setup(unit => unit.PatientRepository).Returns(patientRepository.Object);
            List<Patient> patientsList = new List<Patient>();
            
            Patient p1 = new Patient("Pera","Peric","gmail1@gmail.com",
                                            "11111111","420420",new DateTime(2000,2,2),
                                            "Mike Mikica",BloodType.ZERO_NEGATIVE);
            Patient p2 = new Patient("Mika","Mikic","gmail2@gmail.com",
                                            "22222222","696969",new DateTime(2000,2,2),
                                            "Pere Perica" ,BloodType.ZERO_NEGATIVE);
            Patient createdPatient = new Patient("Zika", "Zikic", "gmail3@gmail.com",
                "99999999", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE);
            
            patientsList.Add(p1);
            patientsList.Add(p2);
            IEnumerable<Patient> patientsEnumerable = patientsList.AsEnumerable();

            if (unique.Equals("throwEmail"))
            {
                personRepository.Setup(unit => unit.GetOneByUid("asdasd")).Returns((Patient)null);
                personRepository.Setup(unit => unit.GetOneByEmail("gmail2@gmail.com")).Returns(p1);
            }
            else if (unique.Equals("throwUid"))
            {
                personRepository.Setup(unit => unit.GetOneByUid("11111111")).Returns(p1);
                personRepository.Setup(unit => unit.GetOneByEmail("gmail3@gmail.com")).Returns((Patient)null);
            }
            else
            {
                personRepository.Setup(unit => unit.GetOneByUid("asdasd")).Returns((Patient)null);
                personRepository.Setup(unit => unit.GetOneByEmail("gmail3@gmail.com")).Returns((Patient)null);
            }

            patientRepository.Setup(unit => unit.GetAll()).ReturnsAsync(patientsEnumerable);
            PatientService patientService = new PatientService(unitOfWork.Object);
            return patientService;
        }
        
        [Fact]
        public void Create_patient_not_unique_uid_Exception()
        {
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail3@gmail.com",
                "11111111", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE);

            Should.Throw<BadRequestException>(() => PatientServiceSetup("throwUid").Create(patientToCreate))
                .Message.ShouldBe("Uid is already taken");
        }
        [Fact]
        public void Create_patient_not_unique_email_Exception()
        {
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail2@gmail.com",
                "99999999", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE);

            Should.Throw<BadRequestException>(() => PatientServiceSetup("throwEmail").Create(patientToCreate))
                .Message.ShouldBe("Email is already taken");
        }
    }
}
