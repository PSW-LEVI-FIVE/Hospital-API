using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Renovation.Interface;
using HospitalLibrary.Renovation.Model;
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
            
            var pending = await _renovationService.GetLatest(DateTime.Now,1);
            return Ok(pending);
        }
        [HttpPost]
        [Route("timeslot/")]
        public async Task<IActionResult> GetTimeSlots([FromBody] TimeSlotReqDTo reqdto)
        {
            var slots = await _renovationService.GenerateCleanerTimeSlots(new TimeInterval(reqdto.startDate, reqdto.endDate), reqdto.duration, reqdto.roomId);
            return Ok(slots);
        }
        [HttpPost]
        [Route("Create/Merge")]
        public async Task<IActionResult> CreateMerge([FromBody] MergeDTO mergeDto)
        {
            var reno = mergeDto.MapToModel();
            var slots = await _renovationService.Create(reno);
            return Ok(slots);
        }
        [HttpPost]
        [Route("Create/Split")]
        public async Task<IActionResult> CreateSplit([FromBody] SplitDTO splitDto)
        {
            var reno = splitDto.MapToModel();

            var slots = await _renovationService.Create(reno);
            return Ok(slots);
        }


        [HttpGet]
        [Route("Testing/")]
        public async Task<IActionResult> GetTest()
        {
            var pending = await _renovationService.GetAllPending();
            await _renovationService.initiateRenovation(pending[0]);
            return Ok(pending);
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
