using HospitalLibrary.Appointments;
using HospitalLibrary.Renovations.Interface;
using HospitalLibrary.Renovations.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation;

namespace HospitalAPI.Controllers.Intranet
{
  [Route("api/intranet/renovation")]
  [ApiController]
  public class RenovationController : Controller
  {
    private readonly IRenovationService _renovationService;
    private readonly IRenovationStatistics _renovationStatics;

    public RenovationController(IRenovationService renovationService, IRenovationStatistics renovationStatics)
    {
      _renovationService = renovationService;
      _renovationStatics = renovationStatics;
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

    [HttpPost]
    [Route("create/event")]
    public async Task<IActionResult> Create([FromBody] RenovationEventCreateDTO renovationDto)
    {
      var reno = await _renovationService.CreateEvent(renovationDto.MapToModel());
      return Ok(reno);
    }

    [HttpPost]
    [Route("add/event")]
    public async Task<IActionResult> AddEvent([FromBody] RenovationAddEventDTO renovationEventDto)
    {
      _renovationService.AddEvent(renovationEventDto.MapToModel());
      return Ok();
    }

    [HttpPost]
    [Route("update/event/")]
    public async Task<IActionResult> UpdateMergeEvent([FromBody] RenovationEventDTO renovationDto)
    {
      var renovation= await _renovationService.UpdateEvent(renovationDto.MapToModel(), renovationDto.Uuid);
      return Ok(renovation);
    }


    [HttpGet]
    [Route("pending/")]
    public async Task<IActionResult> GetAllPending()
    {
      var pending = await _renovationService.GetAllPending();
      return Ok(pending);
    }

    [HttpGet]
    [Route("statistics/avg-step-count")]
    public IActionResult GetAvgStepCount()
    {
     var pending = _renovationStatics.GetAvgStepCount();
     return Ok(pending);
    }

    [HttpGet]
    [Route("statistics/visit-count-merge")]
    public IActionResult GetMergeVisitCount()
    {
      var pending = _renovationStatics.GetTotalVisitsToStep(RenovationType.MERGE);
      return Ok(pending);
    }

    [HttpGet]
    [Route("statistics/visit-count-split")]
    public IActionResult GetSplitVisitCount()
    {
      var pending = _renovationStatics.GetTotalVisitsToStep(RenovationType.MERGE);
      return Ok(pending);
    }

    [HttpGet]
    [Route("statistics/avg-step-time-merge")]
    public IActionResult GetAvgStepTimeMerge()
    {
      var pending = _renovationStatics.GetAverageTimeForStep(RenovationType.MERGE);
      return Ok(pending);
    }
    [HttpGet]
    [Route("statistics/avg-step-time-split")]
    public IActionResult GetAvgStepTimeSplit()
    {
      var pending = _renovationStatics.GetAverageTimeForStep(RenovationType.SPLIT);
      return Ok(pending);
    }
    [HttpGet]
    [Route("statistics/avg-time")]
    public IActionResult GetAvgTime()
    {
      var pending = _renovationStatics.GetAvgTime();
      return Ok(pending);
    }

  }


}
