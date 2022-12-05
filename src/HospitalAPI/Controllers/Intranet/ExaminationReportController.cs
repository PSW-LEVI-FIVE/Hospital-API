using System.Linq;
using System.Security.Claims;
using HospitalLibrary.Examination;
using HospitalLibrary.Examination.Dtos;
using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [ApiController, Route("api/intranet/examination")]
    [Authorize("Doctor")]
    public class ExaminationReportController: ControllerBase
    {
        private IExaminationReportService _examinationReportService;


        public ExaminationReportController(IExaminationReportService examinationReportService)
        {
            _examinationReportService = examinationReportService;
        }


        [HttpGet]
        [Route("report/{id:int}")]
        public IActionResult GetById(int id)
        {
            ExaminationReport report = _examinationReportService.GetById(id);
            return Ok(report);
        }

        [HttpGet]
        [Route("{id:int}/report")]
        public IActionResult GetByExamination(int id)
        {
            ExaminationReport report = _examinationReportService.GetByExamination(id);
            return Ok(report);
        }

        [HttpPost]
        [Route("report")]
        public IActionResult Create(CreateExaminationReportDTO reportDto)
        {
            reportDto.DoctorId = GetCurrentUser().Id;
            ExaminationReport report = _examinationReportService.Create(reportDto.MapToModel());
            return Ok(report);
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
        
        
        
        
    }
}