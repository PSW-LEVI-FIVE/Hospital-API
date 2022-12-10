using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Repositories;
using HospitalLibrary.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace HospitalTests.Units.EquipmentSearch
{
    public class EquipmentNameSearchTest
    {
        private Mock<IUnitOfWork> RoomEquipmentRepositoryMock()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var roomEquipmentRepository = new Mock<IRoomEquipmentRepository>();
            unitOfWork.Setup(unit => unit.RoomEquipmentRepository).Returns(roomEquipmentRepository.Object);
            
            RoomEquipment re1 = new RoomEquipment(12, 12,"Scanner", 12);
            RoomEquipment re2 = new RoomEquipment(13, 12, "Something", 12);
            List<RoomEquipment> reList = new List<RoomEquipment>();
            reList.Add(re1);
            reList.Add(re2);
            IEnumerable<RoomEquipment> roomEquipment = reList.AsEnumerable();
            roomEquipmentRepository.Setup( r => r.GetAll()).ReturnsAsync(roomEquipment);
          
            return unitOfWork;
        }

        [Fact]
        public void Can_find_equipment_by_combine_search()
        {
            var mock = RoomEquipmentRepositoryMock();
            var dto = new RoomEquipmentDTO("Scanner", 11, 12);
            IRoomEquipmentValidator validator = new RoomEquipmentValidator(mock.Object);
            var roomEquipmentService = new RoomEquipmentService(mock.Object, validator);
            var result = (IEnumerable<RoomEquipment>) roomEquipmentService.SearchEquipmentInRoom(dto).Result;

            result.ShouldNotBeNull();
        }

        [Fact]
        public void Can_find_equipment_by_name_search()
        {
            var mock = RoomEquipmentRepositoryMock();
            var dto = new RoomEquipmentDTO("Scanner", 0, 12);
            IRoomEquipmentValidator validator = new RoomEquipmentValidator(mock.Object);
            var roomEquipmentService = new RoomEquipmentService(mock.Object, validator);
            var result = (IEnumerable<RoomEquipment>)roomEquipmentService.SearchEquipmentInRoom(dto).Result;

            result.ShouldNotBeNull();
        }

        [Fact]
        public void Can_find_equipment_by_quantity_search()
        {
            var mock = RoomEquipmentRepositoryMock();
            var dto = new RoomEquipmentDTO(" ", 5, 12);
            IRoomEquipmentValidator validator = new RoomEquipmentValidator(mock.Object);
            var roomEquipmentService = new RoomEquipmentService(mock.Object, validator);
            var result = (IEnumerable<RoomEquipment>)roomEquipmentService.SearchEquipmentInRoom(dto).Result;

            result.ShouldNotBeNull();
        }

        [Fact]
        public void Can_not_find_equipment_by_combine_search()
        {
            var mock = RoomEquipmentRepositoryMock();
            var dto = new RoomEquipmentDTO("w", 30, 9);
            IRoomEquipmentValidator validator = new RoomEquipmentValidator(mock.Object);
            var roomEquipmentService = new RoomEquipmentService(mock.Object, validator);
            var result = (IEnumerable<RoomEquipment>)roomEquipmentService.SearchEquipmentInRoom(dto).Result;

            result.ShouldBeEmpty();
        }

        [Fact]
        public void Can_not_find_equipment_by_name_search()
        {
            var mock = RoomEquipmentRepositoryMock();
            var dto = new RoomEquipmentDTO("w", 0, 12);
            IRoomEquipmentValidator validator = new RoomEquipmentValidator(mock.Object);
            var roomEquipmentService = new RoomEquipmentService(mock.Object, validator);
            var result = (IEnumerable<RoomEquipment>)roomEquipmentService.SearchEquipmentInRoom(dto).Result;

            result.ShouldBeEmpty();
        }

        [Fact]
        public void Can_not_find_equipment_by_quantity_search()
        {
            var mock = RoomEquipmentRepositoryMock();
            var dto = new RoomEquipmentDTO(" ", 12, 12);
            IRoomEquipmentValidator validator = new RoomEquipmentValidator(mock.Object);
            var roomEquipmentService = new RoomEquipmentService(mock.Object, validator);
            var result = (IEnumerable<RoomEquipment>)roomEquipmentService.SearchEquipmentInRoom(dto).Result;

            result.ShouldBeEmpty();
        }
    }
}
