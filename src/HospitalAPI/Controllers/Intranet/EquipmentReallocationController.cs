using HospitalLibrary.Appointments.Dtos;
using HospitalLibrary.Rooms.DTOs;
using HospitalLibrary.Rooms.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet]
        public async Task<IActionResult> GetAvailableIntervals([FromBody] CreateIntervalsEquipmentReallocationDTO reallocationDTO)
        {
            TimeSpan s=new TimeSpan(0, reallocationDTO.duration, 0);
            await _equipmentReallocationService.GetPossibleInterval(reallocationDTO.StartingRoomId, reallocationDTO.DestinationRoomId,reallocationDTO.date, s);
            return Ok();
        }

        // GET api/<EquipmentReallocationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EquipmentReallocationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EquipmentReallocationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EquipmentReallocationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
