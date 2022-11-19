using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Create([FromBody] CreateFeedbackDTO createFeedbackDto)
        {
            Feedback created = _feedbackService.Create(createFeedbackDto.MapToModel());
            return Ok(created);
        }
        
        [HttpGet]
        [Route("published")]
        public async Task<IActionResult> GetPublished()
        {
            IEnumerable<PublishedFeedbackDTO> publishedFeedbacks = await _feedbackService.GetPublishedFeedbacks();
            return Ok(publishedFeedbacks);
        }
    }
}
