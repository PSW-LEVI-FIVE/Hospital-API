using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Examination;
using HospitalLibrary.Symptoms;
using HospitalLibrary.Symptoms.Dtos;
using HospitalLibrary.Symptoms.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [ApiController, Route("api/intranet/symptoms")]
    [Authorize(Roles="Doctor")]
    public class SymptomsController: ControllerBase
    {

        private ISymptomService _symptomService;

        public SymptomsController(ISymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        [HttpPost]
        public IActionResult Create(CreateSymptomDto symptomDto)
        {
            Symptom symptom = _symptomService.Create(symptomDto.MapToModel());
            return Ok(symptom);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Symptom> symptoms = await _symptomService.GetAll();
            return Ok(symptoms);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search([FromQuery] string name)
        {
            if (name == null || name.Trim().Equals("")) 
                return Ok(new List<Symptom>());
            IEnumerable<Symptom> symptoms = _symptomService.Search(name);
            return Ok(symptoms);
        }
    }
}