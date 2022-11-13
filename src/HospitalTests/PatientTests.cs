using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
using Moq;
using Shouldly;

namespace HospitalTests
{
    
    public class InitTest
    {
        public Mock<IUnitOfWork> PatientRepositorySetup()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var patientRepository = new Mock<IPatientRepository>();
            unitOfWork.Setup(unit => unit.PatientRepository).Returns(patientRepository.Object);
            List<Patient> patientsList = new List<Patient>();
            patientsList.Add(new Patient("Pera","Peric","gmail@gmail.com","234234","420420",new DateTime(),"Mike Mikica"));
            patientsList.Add(new Patient("Mika","Mikic","gmail@gmail.com","123123","696969",new DateTime(),"Pere Perica"));
            IEnumerable<Patient> patientsEnumerable = patientsList.AsEnumerable();
            patientRepository.Setup(unit => unit.GetAll()).ReturnsAsync(patientsEnumerable);
            return unitOfWork;
        }

        [Fact]
        public async Task Finds_all_patients()
        {
            PatientService patientservice = new PatientService(PatientRepositorySetup().Object);
            IEnumerable<Patient> patients = await patientservice.GetAll();
            patients.ShouldNotBeEmpty();
        }
    }
}
