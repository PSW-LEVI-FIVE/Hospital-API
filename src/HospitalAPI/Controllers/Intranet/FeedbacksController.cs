<<<<<<< HEAD
﻿using HospitalLibrary.Feedbacks;
using HospitalLibrary.Feedbacks.Dtos;
=======
﻿using System;
using HospitalLibrary.Feedbacks;
>>>>>>> 4896b80 (Show feedback through DTO)
using HospitalLibrary.Feedbacks.Interfaces;
using HospitalLibrary.Rooms;
using HospitalLibrary.Rooms.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            Feedback created =  _feedbackService.Create(feedback);
            return Ok(created);
        }
        
        
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Feedback> feedbacks = await _feedbackService.GetAll();
            List<AnonymousFeedbackDTO> anonymousFeedbacks = new List<AnonymousFeedbackDTO>();
            foreach (Feedback feedback in feedbacks)
            {
                Patient tempPatient = _feedbackService.getPatientById(feedback.PatientId);
                anonymousFeedbacks.Add(new AnonymousFeedbackDTO(feedback.Id,
                    tempPatient.Name + " " + tempPatient.Surname,
                    feedback.FeedbackContent));
            }

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
