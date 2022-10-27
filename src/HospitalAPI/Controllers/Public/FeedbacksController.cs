using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Feedbacks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public IActionResult Create(Feedback feedback)
        {
            Feedback created = _feedbackService.Create(feedback);
            return Ok(created);
        }
    }
}
