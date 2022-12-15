using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Renovation.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/renovation")]
    [ApiController]
    public class RenovationController : ControllerBase
    {
        private readonly IRenovationService _renovationService;

        public RenovationController(IRenovationService renovationService)
        {
            _renovationService = renovationService;
        }
        [HttpGet]
        [Route("latest/")]
        public async Task<IActionResult> GetLatest()
        {
            var pending = await _renovationService.GetLatest(DateTime.Now);
            return Ok(pending);
        }
        [HttpGet]
        [Route("timeslot/")]
        public async Task<IActionResult> GetTimeSlots()
        {
            var slots = await _renovationService.GenerateTimeSlots(new TimeInterval(DateTime.Now,DateTime.Now.AddDays(5)),2);
            return Ok(slots);
        }



        [HttpGet]
        [Route("pending/")]
        public async Task<IActionResult> GetAllPending()
        {
            var pending= await _renovationService.GetAllPending();
            return Ok(pending);
        }

   
    }
}
