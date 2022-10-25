using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {

        private IFeedbackService _feedbackService;
        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Feedback NewFeedback)
        {
             _feedbackService.Add(NewFeedback);
            return Ok();
        }
    }
}
