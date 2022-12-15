using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/feedbacks")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {

        private IFeedbackService _feedbackService;
        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        [Authorize(Roles="Patient")]
        public async Task<IActionResult> Create([FromBody] CreateFeedbackDTO createFeedbackDto)
        {
            createFeedbackDto.PatientId = GetCurrentUser().Id;
            Feedback created = await _feedbackService.Create(createFeedbackDto.MapToModel());
            return Ok(created);
        }
        
        [HttpGet]
        [Route("published")]
        public async Task<IActionResult> GetPublished()
        {
            IEnumerable<PublishedFeedbackDTO> publishedFeedbacks = await _feedbackService.GetPublishedFeedbacks();
            return Ok(publishedFeedbacks);
        }
        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null) return null;
            var userClaims = identity.Claims;
            return new UserDTO
            {
                Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                Role = Role.Patient,
                Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
            };
        }
    }
}
