using Feedback = HospitalLibrary.Feedbacks.Feedback;
using HospitalLibrary.Feedbacks.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Patients;

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
        
        [HttpPost]
        public IActionResult Create(Feedback feedback)
        {
            Feedback created = _feedbackService.Create(feedback);
            return Ok(created);
        }

        [HttpGet]
        public async Task<IActionResult> GetManagersFeedbacks()
        {
            IEnumerable<ManagersFeedbackDto> managersFeedbacks = await _feedbackService.GetManagersFeedbacks();
            return Ok(managersFeedbacks);
        }
        
        [HttpPut]
        [Route("{id}")]
        public IActionResult ChangePublishmentStatus(int id, [FromBody] bool feedbackPublishmentStatus)
        {
            Feedback feedback = _feedbackService.Get(id);
            feedback.Update(feedbackPublishmentStatus);
            Feedback updated = _feedbackService.ChangePublishmentStatus(feedback);
            return Ok(updated);
        }
        
    }
}
