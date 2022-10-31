using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Feedback> feedbacks = await _feedbackService.GetAll();
            return Ok(feedbacks);
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
