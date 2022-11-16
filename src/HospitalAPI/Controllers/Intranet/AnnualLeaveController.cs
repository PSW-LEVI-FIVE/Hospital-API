using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Dtos;
using HospitalLibrary.AnnualLeaves.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/annual-leaves")]
    [ApiController]
    public class AnnualLeaveController : ControllerBase
    {
        private IAnnualLeaveService _annualLeaveService;

        public AnnualLeaveController(IAnnualLeaveService annualLeaveService)
        {
            _annualLeaveService = annualLeaveService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnnualLeaveDto annualLeaveDto)
        {
            AnnualLeave leave = await _annualLeaveService.Create(annualLeaveDto.MapToModel());
            return Ok(leave);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetAllByDoctorId(int id)
        {
            IEnumerable<AnnualLeave> annualLeaves = _annualLeaveService.GetAllByDoctorId(id);
            return Ok(annualLeaves);
        }

        [Route("cancel/{id:int}&{docId:int}")]
        [HttpPatch]
        public IActionResult Cancel(int id,int docId)
        {
            AnnualLeave leave = _annualLeaveService.Delete(id, docId);
            return Ok(leave);
        }

    }
}
