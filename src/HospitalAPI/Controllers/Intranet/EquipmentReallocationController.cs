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
            TimeSpan s=new TimeSpan(0, reallocationDTO.duration, 0);
            IEnumerable<TimeInterval> availableIntervals = _equipmentReallocationService.GetPossibleInterval(reallocationDTO.StartingRoomId, reallocationDTO.DestinationRoomId,reallocationDTO.date, s);
            return Ok(availableIntervals);
        }

        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
