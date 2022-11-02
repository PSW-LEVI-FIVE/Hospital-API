using AnonymousFeedbackDTO = HospitalLibrary.Feedbacks.Dtos.AnonymousFeedbackDTO;
using Feedback = HospitalLibrary.Feedbacks.Feedback;
using HospitalLibrary.Feedbacks.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            Feedback created =  _feedbackService.Create(feedback);
            return Ok(created);
        }
        
        
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Feedback> feedbacks = await _feedbackService.GetAll();
            List<AnonymousFeedbackDTO> anonymousFeedbacks = _feedbackService.anonymousList(feedbacks);
            return Ok(anonymousFeedbacks);
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
