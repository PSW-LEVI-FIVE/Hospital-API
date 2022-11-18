using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Appointments;
using HospitalLibrary.Managers;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using HospitalLibrary.Patients;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Managers.Dtos;
using Shouldly;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Patients.Interfaces;

namespace HospitalTests.Units.Managers
{
    public class PatientsPickingDoctorsBasedOnAgeTests
    {
        public ManagerService ManagerServiceSetup(string Case)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var appointmentsRepository = new Mock<IAppointmentRepository>();
            var patientsRepository = new Mock<IPatientRepository>();

            var appointments = new List<Appointment>();
            var patients = new List<Patient>();
            Patient p = new Patient("Pera", "Peric", "gmail1@gmail.com", "11111111", "420420", new DateTime(2000, 2, 2), "Mike Mikica", BloodType.ZERO_NEGATIVE);
            p.Id = 6;   //Is between 18 and 25
            Patient p1 = new Patient("Ivan", "Ivic", "gmail1@gmail.com", "23123213", "420420", new DateTime(2001, 2, 2), "Mike Mikica", BloodType.ZERO_NEGATIVE);
            p.Id = 1;   //Is between 18 and 25
            Patient p2 = new Patient("Sava", "Savic", "gmail1@gmail.com", "11411111", "420420", new DateTime(1960, 2, 2), "Mike Mikica", BloodType.ZERO_NEGATIVE);
            p.Id = 2;   //Is NOT between 18 and 25
            Patient p3 = new Patient("Milan", "Milic", "gmail1@gmail.com", "11111151", "420420", new DateTime(1930, 2, 2), "Mike Mikica", BloodType.ZERO_NEGATIVE);
            p.Id = 3;   //Is NOT between 18 and 25

            patients.Add(p);
            patients.Add(p1);
            patients.Add(p2);
            patients.Add(p3);

            appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());


            switch (Case)
            {
                case "doctor2(18-25)":
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 3, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 6, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    break;
                case "NoPeopleInRange(18-25)":
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 3, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    break;
                case "doctor1and2(18-25)":
                    appointments.Add(new CreateAppointmentDTO(1, 1, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 6, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(3, 3, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 6, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 1, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    break;
                case "doctor1":
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 3, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 6, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    break;
                case "doctor2":
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 3, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 4, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    break;
            }
            appointmentsRepository.Setup(u => u.GetAll()).ReturnsAsync(appointments.AsEnumerable());
            unitOfWork.Setup(u => u.AppointmentRepository).Returns(appointmentsRepository.Object);
            patientsRepository.Setup(u => u.GetAll()).ReturnsAsync(patients.AsEnumerable());
            unitOfWork.Setup(u => u.PatientRepository).Returns(patientsRepository.Object);
            return new ManagerService(unitOfWork.Object);
        }

        [Fact]
        public void One_Most_Popular_Doctor_In_Range()
        {
            ManagerService managerService = ManagerServiceSetup("doctor2(18-25)");

            IEnumerable<DoctorWithPopularityDTO> docsPopularity = (IEnumerable<DoctorWithPopularityDTO>)managerService.GetMostPopularDoctorByAgeRange(18,25);

            docsPopularity.ShouldNotBeEmpty();
            docsPopularity.First().Id.ShouldBe(2);
        }
        [Fact]
        public void No_People_In_Input_Range()
        {
            ManagerService managerService = ManagerServiceSetup("NoPeopleInRange(18-25)");

            IEnumerable<DoctorWithPopularityDTO> docsPopularity = (IEnumerable<DoctorWithPopularityDTO>)managerService.GetMostPopularDoctorByAgeRange(18, 25);

            docsPopularity.ShouldBeEmpty();
        }

        [Fact]
        public void Two_Equaly_Popular_Doctors_In_Range()
        {
            ManagerService managerService = ManagerServiceSetup("doctor1and2(18-25)");

            IEnumerable<DoctorWithPopularityDTO> docsPopularity = (IEnumerable<DoctorWithPopularityDTO>)managerService.GetMostPopularDoctors();

            docsPopularity.ShouldNotBeEmpty();
            docsPopularity.Count().ShouldBe(2);
        }

        [Fact]
        public void One_Most_Popular_Doctor()
        {
            ManagerService managerService = ManagerServiceSetup("doctor1");

            IEnumerable<DoctorWithPopularityDTO> docsPopularity = (IEnumerable<DoctorWithPopularityDTO>)managerService.GetMostPopularDoctors();

            docsPopularity.ShouldNotBeEmpty();
            docsPopularity.First().Id.ShouldBe(1);
        }

        [Fact]
        public void One_Most_Popular_Doctor_Same_Patient_Problem()
        {
            ManagerService managerService = ManagerServiceSetup("doctor2");

            IEnumerable<DoctorWithPopularityDTO> docsPopularity = (IEnumerable<DoctorWithPopularityDTO>)managerService.GetMostPopularDoctors();

            docsPopularity.ShouldNotBeEmpty();
            docsPopularity.First().Id.ShouldBe(2);
        }
    }
}
