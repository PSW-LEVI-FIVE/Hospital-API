using Feedback = HospitalLibrary.Feedbacks.Feedback;
using HospitalLibrary.Feedbacks.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Patients;
using Microsoft.AspNetCore.Authorization;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/feedbacks")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {

        private IFeedbackService _feedbackService;
        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        
        [HttpGet]
        [Route("manager")]
        [Authorize(Roles="Manager")]
        public async Task<IActionResult> GetManagersFeedbacks()
        {
            IEnumerable<ManagersFeedbackDTO> managersFeedbacks = await _feedbackService.GetManagersFeedbacks();
            return Ok(managersFeedbacks);
        }
        
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles="Manager")]
        public IActionResult ChangePublishmentStatus(int id, [FromBody] bool feedbackPublishmentStatus)
        {
            Feedback feedback = _feedbackService.Get(id);
            feedback.Update(feedbackPublishmentStatus);
            Feedback updated = _feedbackService.ChangePublishmentStatus(feedback);
            return Ok(updated);
        }
        
    }
}
