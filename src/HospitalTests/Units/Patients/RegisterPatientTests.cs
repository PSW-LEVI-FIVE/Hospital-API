using System.Data.SqlClient;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Validators;
using Moq;
using Shouldly;

namespace HospitalTests.Units.Patients
{
    public class InitTest
    {
        public PatientService PatientServiceSetup(string unique)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var patientRepository = new Mock<IPatientRepository>();
            unitOfWork.Setup(unit => unit.PatientRepository).Returns(patientRepository.Object);
            List<Patient> patientsList = new List<Patient>();
            
            Patient p2 = new Patient("Pera","Peric","gmail1@gmail.com",
                                            "11111111","420420",new DateTime(2000,2,2),
                                            "Mike Mikica",BloodType.ZERO_NEGATIVE);
            Patient p1 = new Patient("Mika","Mikic","gmail2@gmail.com",
                                            "22222222","696969",new DateTime(2000,2,2),
                                            "Pere Perica" ,BloodType.ZERO_NEGATIVE);
            patientsList.Add(p1);
            patientsList.Add(p2);
            IEnumerable<Patient> patientsEnumerable = patientsList.AsEnumerable();
            
            patientRepository.Setup(unit => unit.GetAll()).ReturnsAsync(patientsEnumerable);
            if (unique.Equals(""))
            {
                patientRepository.Setup(unit => 
                    unit.GetOneByUid("11111111")).ReturnsAsync((Patient)null);
                patientRepository.Setup(unit => 
                    unit.GetOneByEmail("gmail1@gmail.com")).ReturnsAsync((Patient)null);
                
            }else if(unique.Equals("uid"))
            {
                patientRepository.Setup(unit =>
                    unit.GetOneByUid("11111111")).ReturnsAsync(p1);
                patientRepository.Setup(unit => 
                    unit.GetOneByEmail("gmail1@gmail.com")).ReturnsAsync((Patient)null);
            }
            else
            {
                patientRepository.Setup(unit => 
                    unit.GetOneByUid("11111111")).ReturnsAsync((Patient)null);
                patientRepository.Setup(unit =>
                    unit.GetOneByEmail("gmail1@gmail.com")).ReturnsAsync(p1);
            }

            PatientService patientService = new PatientService(unitOfWork.Object,
                new PatientRegistrationValidationService(unitOfWork.Object));
            return patientService;
        }

        [Fact]
        public async Task Finds_all_patients()
        {
            PatientService patientservice = PatientServiceSetup("");
            IEnumerable<Patient> patients = await patientservice.GetAll();
            patients.ShouldNotBeEmpty();
        }
        [Fact]
        public async void Create_patient_success()
        {
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail3@gmail.com",
                "99999999", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE);

            Patient createdPatient = await PatientServiceSetup("").Create(patientToCreate);
            createdPatient.ShouldNotBeNull();
        }
        [Fact]
        public void Create_patient_not_unique_uid_Exception()
        {
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail3@gmail.com",
                "11111111", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE);

            Should.Throw<BadRequestException>(() => PatientServiceSetup("uid").Create(patientToCreate))
                .Message.ShouldBe("Uid is already taken");
        }
        [Fact]
        public void Create_patient_not_unique_email_Exception()
        {
            Patient patientToCreate = new Patient("Zika", "Zikic", "gmail1@gmail.com",
                "99999999", "555555", new DateTime(2000,2,2), "Jovina 12",
                BloodType.ZERO_NEGATIVE);

            Should.Throw<BadRequestException>(() => PatientServiceSetup("mail").Create(patientToCreate))
                .Message.ShouldBe("Email is already taken");
        }
    }
}
