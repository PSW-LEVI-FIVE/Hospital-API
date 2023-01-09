using HospitalLibrary.Appointments;
using HospitalLibrary.Renovations.Interface;
using HospitalLibrary.Renovations.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
    [Route("update/event")]
    public async Task<IActionResult> UpdateEvent([FromBody] RenovationEventCreateDTO renovationDto,string uuid)
    {
      var renovation= _renovationService.UpdateEvent(renovationDto.MapToModel(), uuid);
      return Ok(renovation);
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
