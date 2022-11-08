using HospitalLibrary.Doctors.Interfaces;
using HospitalLibrary.Patients;
using HospitalLibrary.Patients.Interfaces;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;
using Moq;

namespace HospitalTests;

public class InitTest
{
    [Fact]
    public void Finds_all_patients()
    {
        // var unitOfWork = new Mock<IUnitOfWork>();
        var patientList = new List<Patient>();
        patientList.Add(new Patient("Pera","Peric","gmail@gmail.com","234234","420420",new DateTime(),"Mike Mikica"));
        patientList.Add(new Patient("Mika","Mikic","gmail@gmail.com","123123","696969",new DateTime(),"Pere Perica"));
        // var patients = new Task<IEnumerable<Patient>>();
        // unitOfWork.Setup(unit => unit.PatientRepository.GetAll()).Returns(patients);
        // PatientService patientservice = new PatientService(unitOfWork);
        // patientservice.GetAll();
        //?????
    }
}