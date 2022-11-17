using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/allergen")]
    [ApiController]
    public class AllergenController : ControllerBase
    {
        private IAllergenService _allergenService;

        public AllergenController(IAllergenService allergenService)
        {
            _allergenService = allergenService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAllergenDTO createAllergenDTO)
        {
            Allergen created = _allergenService.Create(createAllergenDTO.MapToModel());
            return Ok(created);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetPublished()
        {
            IEnumerable<Allergen> publishedFeedbacks = await _allergenService.GetAll();
            return Ok(publishedFeedbacks);
        }
    }
}