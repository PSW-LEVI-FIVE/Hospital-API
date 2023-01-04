using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.Examination;
using HospitalLibrary.Examination.Dtos;
using HospitalLibrary.Examination.Interfaces;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Interactions;

namespace HospitalAPI.Controllers.Intranet
{
    [ApiController, Route("api/intranet/examination")]
    [Authorize(Roles="Doctor")]
    public class ExaminationReportController: ControllerBase
    {
        private IExaminationReportService _examinationReportService;
        private IExaminationReportStatistics _examinationReportStatistics;


        public ExaminationReportController(IExaminationReportService examinationReportService, IExaminationReportStatistics examinationReportStatistics)
        {
            _examinationReportService = examinationReportService;
            _examinationReportStatistics = examinationReportStatistics;
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
        public async Task<IActionResult> Create(CreateExaminationReportDTO reportDto)
        {
            reportDto.DoctorId = GetCurrentUser().Id;
            var report = await _examinationReportService.Create(reportDto.MapInitialToModel());
            return Ok(report);
        }

        [HttpPatch]
        [Route("report/{uuid}")]
        public async Task<IActionResult> Update(CreateExaminationReportDTO reportDto, string uuid)
        {
            reportDto.DoctorId = GetCurrentUser().Id;
            ExaminationReport report = await _examinationReportService.Update(reportDto.MapToModel(), uuid);
            return Ok(report);
        }

        [HttpGet]
        [Route("statistics/succ-unsucc")]
        public IActionResult GetSuccUnsuccExaminationReports()
        {
            SuccessfulUnsuccessfulReportsDto dto = _examinationReportStatistics.CalculateSuccessfulUnsuccessfulReports();
            return Ok(dto);
        }

        [HttpGet]
        [Route("statistics/succ-unsucc-spec")]
        public async Task<IActionResult> GetSuccUnsuccExaminationReportsForSpecialties()
        {
            var dtos = await _examinationReportStatistics.CalculateSuccessfulUnsuccessfulPerSpecialty();
            return Ok(dtos);
        }

        [HttpGet]
        [Route("statistics/steps")]
        public IActionResult GetStepStatistics()
        {
            var result = _examinationReportStatistics.CalculateStepsAverageTime();
            return Ok(result);
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