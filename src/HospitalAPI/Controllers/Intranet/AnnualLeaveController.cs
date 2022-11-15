using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Dtos;
using HospitalLibrary.AnnualLeaves.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/annual_leaves")]
    [ApiController]
    public class AnnualLeaveController : ControllerBase
    {
        private IAnnualLeaveService _annualLeaveService;

        public AnnualLeaveController(IAnnualLeaveService annualLeaveService)
        {
            _annualLeaveService = annualLeaveService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] AnnualLeaveDto annualLeaveDto)
        {
            AnnualLeave leave = _annualLeaveService.Create(annualLeaveDto.MapToModel());
            return Ok(leave);
        }
        
    }
}