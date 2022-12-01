using HospitalLibrary.Appointments;
using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Rooms.DTOs;
using HospitalLibrary.Rooms.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/[controller]")]
    [ApiController]
    public class EquipmentReallocationController : ControllerBase
    {
        private readonly IEquipmentReallocationService _equipmentReallocationService;

        public EquipmentReallocationController(IEquipmentReallocationService equipmentReallocationService)
        {
            _equipmentReallocationService = equipmentReallocationService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAvailableIntervals([FromBody] CreateIntervalsEquipmentReallocationDTO reallocationDTO)
        {

            TimeSpan s =new TimeSpan(0, reallocationDTO.duration, 0);  
            var availableIntervals =await _equipmentReallocationService.GetPossibleInterval(reallocationDTO.StartingRoomId, reallocationDTO.DestinationRoomId,reallocationDTO.date, s);

            return Ok(availableIntervals);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> AddNewReallocation([FromBody] CreateEquipmentReallocationDTO createDto)
        {

         
            var availableIntervals = await _equipmentReallocationService.Create(createDto.MapToModel());
            return Ok(availableIntervals);
        }


        [HttpGet("pending")]
        public async Task<IActionResult> GetAllPending()
        {
            var pending = await _equipmentReallocationService.getAllPending();
            return Ok(pending) ;
        }

        [HttpGet("pendingToday")]

        public async Task<IActionResult> GetAllPendingToday()
        {
            var pending = await _equipmentReallocationService.getAllPendingForToday();
            return Ok(pending);
        }

        [HttpGet("reserved_eq/{equipmentId}")]
        public async Task<IActionResult> GetReservedEquipment(int equipmentId)
        {
            var reservedEquipment = await _equipmentReallocationService.getReservedEquipment(equipmentId);
            return Ok(reservedEquipment);
        }
        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> GetEquipmentForRoom(int roomId)
        {
            var RoomEquipment  = await _equipmentReallocationService.getEquipmentByRoom(roomId);
            return Ok(RoomEquipment);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
