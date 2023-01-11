using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.AnnualLeaves;
using HospitalLibrary.AnnualLeaves.Dtos;
using HospitalLibrary.AnnualLeaves.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using HospitalLibrary.Hospitalizations.Dtos;
using HospitalLibrary.Hospitalizations;
using Microsoft.AspNetCore.Mvc;
using HospitalLibrary.Allergens;

namespace HospitalAPI.Controllers.Intranet
{
    // [Authorize]
    [Route("api/intranet/annual-leaves")]
    [ApiController]
    //[Authorize(Roles="Doctor, Manager")]
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
            AnnualLeave annualLeave = annualLeaveDto.MapToModel(GetCurrentUser().Id);
            AnnualLeave leave = await _annualLeaveService.Create(annualLeave);
            return Ok(leave);
        }

        [Route("doctor")]
        [HttpGet]
        public IActionResult GetAllByDoctorId()
        {
            int id = GetCurrentUser().Id;
            IEnumerable<AnnualLeave> annualLeaves = _annualLeaveService.GetAllByDoctorId(id);
            return Ok(annualLeaves);
        }

        [Route("cancel/{id:int}")]
        [HttpPatch]
        public IActionResult Cancel(int id)
        {
            int docId = GetCurrentUser().Id;
            AnnualLeave leave = _annualLeaveService.Delete(id, docId);
            return Ok();
        }
        
        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserDTO
                {
                    Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    Role = Role.Doctor,
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
                };
            }

            return null;
        }

        [Route("pending")]
        [HttpGet]
        public IActionResult GetAllPending()
        {
            IEnumerable<PendingRequestsDTO> annualLeaves = _annualLeaveService.GetAllPending();
            return Ok(annualLeaves);
        }

        [Route("review/{id:int}")]
        [HttpPatch]
        public IActionResult ReviewRequest([FromBody] ReviewLeaveRequestDTO reviewLeaveRequestDto, int id)
        {
            AnnualLeave leave = _annualLeaveService.ReviewRequest(reviewLeaveRequestDto, id);
            return Ok(leave);
        }

        [Route("statistics/{id:int}")]
        [HttpGet]
        public IActionResult GetMonthlyStatisticsByDoctorId(int id)
        {
            IEnumerable<MonthlyLeavesDTO> monthlyLeavesDTOs = _annualLeaveService.GetMonthlyStatisticsByDoctorId(id);
            return Ok(monthlyLeavesDTOs);
        }

    }
}
