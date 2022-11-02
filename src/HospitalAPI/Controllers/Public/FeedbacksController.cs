using System;
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Dtos;
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
        public IActionResult Create([FromBody] CreateFeedbackDto createFeedbackDto)
        {
            Feedback created = _feedbackService.Create(createFeedbackDto.MapToModel());
            return Ok(created);
        }
        
        [HttpGet]
        [Route("published")]
        public async Task<IActionResult> GetPublished()
        {
            IEnumerable<Feedback> feedbacks = await _feedbackService.GetPublished();
            return Ok(feedbacks);
        }
        

    }
}
