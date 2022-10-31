using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public IActionResult Create(Feedback feedback)
        {
            Feedback created =  _feedbackService.Create(feedback);
            return Ok(created);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Feedback> feedbacks = await _feedbackService.GetAll();
            return Ok(feedbacks);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateFeedbackDto updateFeedbackDto)
        {
            Feedback feedback = _feedbackService.Get(id);
            feedback.Update(updateFeedbackDto);
            Feedback updated = _feedbackService.Update(feedback);
            return Ok(updated);
        }
    }
}
