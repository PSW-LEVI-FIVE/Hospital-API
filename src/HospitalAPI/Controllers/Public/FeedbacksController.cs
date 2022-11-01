using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using HospitalLibrary.Appointments.Dtos;

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
