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
            Console.Write(reallocationDTO.duration);  
            var availableIntervals =await _equipmentReallocationService.GetPossibleInterval(reallocationDTO.StartingRoomId, reallocationDTO.DestinationRoomId,reallocationDTO.date, s);
            //public async Task<List<TimeInterval>> GetPossibleInterval(int Starting_roomId, int Destination_roomId, DateTime date, TimeSpan duration)

            return Ok(availableIntervals);
        }

        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
