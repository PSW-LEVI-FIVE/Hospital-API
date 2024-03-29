﻿using HospitalLibrary.Rooms.Dtos;
using HospitalLibrary.Rooms.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/equipment")]
    [ApiController]
    public class RoomEquipmentController : ControllerBase
    {
        private IRoomEquipmentService _roomEquipmentService;

        public RoomEquipmentController(IRoomEquipmentService roomEquipmentService)
        {
            _roomEquipmentService = roomEquipmentService;
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> SearchEquipmentInRoom([FromQuery] RoomEquipmentDTO roomEquipmentDTO)
        {
            var result = await _roomEquipmentService.SearchEquipmentInRoom(roomEquipmentDTO);
            return Ok(result);
        }

        [Route("floor-search")]
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult SearchRoomsByFloorContainingEquipment([FromQuery] RoomEquipmentDTO roomEquipmentDTO)
        {
            var result = _roomEquipmentService.SearchRoomsByFloorContainingEquipment(roomEquipmentDTO);
            return Ok(result);
        }

        [Route("get/{equipmentId}")]
        [HttpGet]
        
        public IActionResult GetEquipment(int equipmentId)
        {
            var equipment = _roomEquipmentService.GetEquipmentById(equipmentId);
            return Ok(equipment);
        }
        [Route("room/{roomId}")]
        [HttpGet]
        public async Task<IActionResult> GetEquipmentForRoom(int roomId)
        {
            var result = await _roomEquipmentService.GetRoomEquipment(roomId);
            return Ok(result);
        }


    }
}
