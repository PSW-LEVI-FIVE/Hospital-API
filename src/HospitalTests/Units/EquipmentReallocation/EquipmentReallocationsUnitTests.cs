﻿using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.DTOs;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
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
using Xunit.Abstractions;

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


            TimeInterval er5 = new TimeInterval(DateTime.Parse("2023-11-23 9:30:00"), DateTime.Parse("2023-11-23 10:00:00"));
            TimeInterval er6 = new TimeInterval(DateTime.Parse("2023-11-23 10:00:00"), DateTime.Parse("2023-11-23 11:00:00"));
            HospitalLibrary.Rooms.Model.EquipmentReallocation rellocation =
                new HospitalLibrary.Rooms.Model.EquipmentReallocation(1, 1,2, 3, 2, ReallocationState.PENDING,
                    DateTime.Parse("2022-12-15 10:30:00"), DateTime.Parse("2022-12-17 10:30:00"));
            List<TimeInterval> reList1 = new List<TimeInterval>();
            List<TimeInterval> reList2 = new List<TimeInterval>();

            List<TimeInterval> apList1 = new List<TimeInterval>();
            List<TimeInterval> apList2 = new List<TimeInterval>();
            List<HospitalLibrary.Rooms.Model.EquipmentReallocation> eqList = new List<HospitalLibrary.Rooms.Model.EquipmentReallocation>();
            reList1.Add(er1);
            reList1.Add(er2);

            apList1.Add(er1);
            apList1.Add(er2);

            reList2.Add(er4);
            apList2.Add(er5);
            
            reList2.Add(er6);
            apList2.Add(er3);
            eqList.Add(rellocation);
            equipmentReallocationRepository.Setup(r => r.GetAllRoomTakenInrevalsForDate(1, DateTime.Parse("2023-11-23 10:30:00"))).ReturnsAsync(reList1);
            appointmentRepository.Setup(r => r.GetAllRoomTakenIntervalsForDate(1, DateTime.Parse("2023-11-23 10:30:00"))).ReturnsAsync(apList1);
            equipmentReallocationRepository.Setup(r => r.GetAll()).ReturnsAsync(eqList);
            equipmentReallocationRepository.Setup(r => r.GetAllRoomTakenInrevalsForDate(2, DateTime.Parse("2023-11-23 10:30:00"))).ReturnsAsync(reList2);
            appointmentRepository.Setup(r => r.GetAllRoomTakenIntervalsForDate(2, DateTime.Parse("2023-11-23 10:30:00"))).ReturnsAsync(apList2);


            return unitOfWork;
        }


        [Fact]
        public void SuccesfullyFoundTakenIntervals()
        {
            var unitOfWork =SetupUOW();
            var dto = new CreateIntervalsEquipmentReallocationDTO(1,2, DateTime.Parse("2023-11-23 10:30:00"),30);
            var roomEquipmentService = new Mock<IRoomEquipmentService>();
            ITimeIntervalValidationService validator = new TimeIntervalValidationService(unitOfWork.Object);
            IEquipmenrRelocationValidator relocationValidator = new EquipmentRelocationValidator();
            EquipmentReallocationService service = new EquipmentReallocationService(unitOfWork.Object, validator, roomEquipmentService.Object,relocationValidator);
            var result = service.GetTakenIntervals(dto.StartingRoomId,dto.date);
            result.ShouldNotBeNull();
        }

        [Fact]
        public void SuccesfullyFoundFreeIntervals()
        {
            var unitOfWork = SetupUOW();
            var dto = new CreateIntervalsEquipmentReallocationDTO(1, 2, DateTime.Parse("2023-11-23 10:30:00"), 30);
            var roomEquipmentService = new Mock<IRoomEquipmentService>();
            ITimeIntervalValidationService validator = new TimeIntervalValidationService(unitOfWork.Object);
            IEquipmenrRelocationValidator relocationValidator = new EquipmentRelocationValidator();
            EquipmentReallocationService service = new EquipmentReallocationService(unitOfWork.Object, validator, roomEquipmentService.Object,relocationValidator);
            var result = service.GetPossibleInterval(dto.StartingRoomId,dto.DestinationRoomId, dto.date,new TimeSpan(0,dto.duration,0));
            result.ShouldNotBeNull();
        }

        [Fact]
        public void UnSuccesfullyFoundFreeIntervals()
        {

            var unitOfWork = SetupUOW();
            var dto = new CreateIntervalsEquipmentReallocationDTO(1, 2, DateTime.Parse("2023-11-23 10:30:00"), 300000000);
            ITimeIntervalValidationService validator = new TimeIntervalValidationService(unitOfWork.Object);
            var roomEquipmentService = new Mock<IRoomEquipmentService>();
            IEquipmenrRelocationValidator relocationValidator = new EquipmentRelocationValidator();
            EquipmentReallocationService service = new EquipmentReallocationService(unitOfWork.Object, validator, roomEquipmentService.Object,relocationValidator);
            var result = service.GetPossibleInterval(dto.StartingRoomId, dto.DestinationRoomId, dto.date, new TimeSpan(0, dto.duration, 0));
            result.Result.Count.ShouldBe(0);
        }

        [Fact]
        public void cancel_equipment_rellocation()
        {
            var unitOfWork = SetupUOW();
            var time = DateTime.Now;
            
            ITimeIntervalValidationService validator = new TimeIntervalValidationService(unitOfWork.Object);
            var roomEquipmentService = new Mock<IRoomEquipmentService>();
            HospitalLibrary.Rooms.Model.EquipmentReallocation rellocation =
                new HospitalLibrary.Rooms.Model.EquipmentReallocation(1, 1,2, 3, 2, ReallocationState.PENDING,
                    DateTime.Parse("2022-12-15 10:30:00"), DateTime.Parse("2022-12-17 10:30:00"));
            IEquipmenrRelocationValidator relocationValidator = new EquipmentRelocationValidator();
            EquipmentReallocationService service = new EquipmentReallocationService(unitOfWork.Object, validator,
                roomEquipmentService.Object, relocationValidator);
            unitOfWork.Setup(work =>
                work.EquipmentReallocationRepository.GetOne(It.IsAny<int>())).Returns(rellocation);
            Should.Throw<HospitalLibrary.Shared.Exceptions.BadRequestException>(() =>
                service.CancelEquipmentRelocation(rellocation.Id));
        }
    }

    
}
