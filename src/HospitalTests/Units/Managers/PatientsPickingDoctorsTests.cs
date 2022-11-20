using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Managers;
using HospitalLibrary.Managers.Dtos;
using HospitalLibrary.Persons;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTests.Units.Managers
{
    public class PatientsPickingDoctorsTests
    {

        public ManagerService ManagerServiceSetup(string Case)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var appointmentsRepository = new Mock<AppointmentRepository>();

            var appointments = new List<Appointment>();
            switch (Case)
            {
                case "doctor1":
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 3, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 4, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 5, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 6, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    break;
                case "noAppointments":
                    break;
                case "doctor2":
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 3, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 4, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    break;
                case "doctor1and2":
                    appointments.Add(new CreateAppointmentDTO(1, 1, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(1, 2, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(3, 5, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 3, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    appointments.Add(new CreateAppointmentDTO(2, 4, 3, new DateTime(2022, 3, 3), new DateTime(2022, 3, 4)).MapToModel());
                    break;
            }
            appointmentsRepository.Setup(u => u.GetAll()).ReturnsAsync(appointments);
            unitOfWork.Setup(u => u.AppointmentRepository).Returns(appointmentsRepository.Object);
            return new ManagerService(unitOfWork.Object);
        }

        [Fact]
        public void One_Most_Popular_Doctor()
        {
            ManagerService managerService = ManagerServiceSetup("doctor1");

            List<DoctorsPopularityDTO> docsPopularity = managerService.GetMostPopularDoctors();

            Assert.Single(docsPopularity);
            Assert.Equal(1, docsPopularity.First().DoctorID);
        }
        [Fact]
        public void No_Appointments()
        {
            ManagerService managerService = ManagerServiceSetup("noAppointments");

            List<DoctorsPopularityDTO> docsPopularity = managerService.GetMostPopularDoctors();

            Assert.Empty(docsPopularity);
        }
        [Fact]
        public void One_Most_Popular_Doctor_Same_Patient_Problem()
        {
            ManagerService managerService = ManagerServiceSetup("doctor2");

            List<DoctorsPopularityDTO> docsPopularity = managerService.GetMostPopularDoctors();

            Assert.Single(docsPopularity);
            Assert.Equal(2, docsPopularity.First().DoctorID);
        }

        [Fact]
        public void Two_Equaly_Popular_Doctors()
        {
            ManagerService managerService = ManagerServiceSetup("doctor1and2");

            List<DoctorsPopularityDTO> docsPopularity = managerService.GetMostPopularDoctors();

            Assert.NotEmpty(docsPopularity);
            Assert.Equal(2, docsPopularity.Count);
        }

    }
}
