
using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Renovations.Interface;
using HospitalLibrary.Renovations.Model;
using Microsoft.AspNetCore.Mvc;
namespace HospitalAPI.Controllers.Intranet
{
  [Route("api/intranet/renovation")]
  [ApiController]
  public class RenovationController : Controller
  {
    private readonly IRenovationService _renovationService;

    public RenovationController(IRenovationService renovationService)
    {
      _renovationService = renovationService;
    }
    [HttpPost]
    [Route("timeslot/")]
    public async Task<IActionResult> GetTimeSlots([FromBody] TimeSlotReqDTo reqdto)
    {
      var slots = await _renovationService.GenerateCleanerTimeSlots(new TimeInterval(reqdto.StartDate, reqdto.EndDate), reqdto.Duration, reqdto.RoomId);
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
    [Route("pending/")]
    public async Task<IActionResult> GetAllPending()
    {
      var pending = await _renovationService.GetAllPending();
      return Ok(pending);
    }


  }


}
