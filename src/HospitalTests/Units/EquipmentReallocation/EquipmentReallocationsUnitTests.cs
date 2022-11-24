using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.DTOs;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Repositories;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Shared.Validators;
using HospitalLibrary.Users.Interfaces;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTests.Units.EquipmentReallocation
{
    public class EquipmentReallocationsUnitTests
    {

        private Mock<IUnitOfWork> SetupUOW()
        {
            var unitOfWork = new Mock<IUnitOfWork>();

            var equipmentReallocationRepository = new Mock<IEquipmentReallocationRepository>();
            var appointmentRepository = new Mock<IAppointmentRepository>();

            unitOfWork.Setup(u => u.EquipmentReallocationRepository).Returns(equipmentReallocationRepository.Object);
            unitOfWork.Setup(u => u.AppointmentRepository).Returns(appointmentRepository.Object);

            TimeInterval er1 = new TimeInterval(DateTime.Parse("2023-11-23 10:30:00"),DateTime.Parse("2023-11-23 11:30:00"));
            TimeInterval er2 = new TimeInterval(DateTime.Parse("2023-11-23 14:30:00"), DateTime.Parse("2023-11-23 15:30:00"));

            TimeInterval er3 = new TimeInterval(DateTime.Parse("2023-11-23 12:30:00"), DateTime.Parse("2023-11-23 13:00:00"));
            TimeInterval er4 = new TimeInterval(DateTime.Parse("2023-11-23 16:30:00"), DateTime.Parse("2023-11-23 17:30:00"));
            List<TimeInterval> reList = new List<TimeInterval>();
            List<TimeInterval> apList = new List<TimeInterval>();

            reList.Add(er1);
            reList.Add(er2);

            apList.Add(er1);
            apList.Add(er2);

            equipmentReallocationRepository.Setup(r => r.GetAllRoomTakenInrevalsForDate(1, DateTime.Parse("2023-11-23 10:30:00"))).ReturnsAsync(reList);
            appointmentRepository.Setup(r => r.GetAllRoomTakenIntervalsForDate(1, DateTime.Parse("2023-11-23 10:30:00"))).ReturnsAsync(apList);


            return unitOfWork;
        }


        [Fact]
        public void SuccesfullyFoundFreeIntervals()
        {

            var unitOfWork =SetupUOW();
            var dto = new CreateIntervalsEquipmentReallocationDTO(1,2, DateTime.Parse("2023-11-23 10:30:00"),30);
            ITimeIntervalValidationService validator = new TimeIntervalValidationService(unitOfWork.Object);
            EquipmentReallocationService service = new EquipmentReallocationService(unitOfWork.Object, validator);
            var result = service.GetTakenIntevals(dto.StartingRoomId,dto.date);
            result.ShouldNotBeNull();
        }


    }

    
}
