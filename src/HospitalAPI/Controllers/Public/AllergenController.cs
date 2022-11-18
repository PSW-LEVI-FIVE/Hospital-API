using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public IActionResult Create([FromBody] AllergenDTO allergenDto)
        {
            Allergen created = _allergenService.Create(allergenDto.MapToModel());
            return Ok(created);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Allergen> allergens = await _allergenService.GetAll();
            List<AllergenDTO> allergensDTO = new List<AllergenDTO>();
            foreach (Allergen allergen in allergens.ToList()) {
                allergensDTO.Add(new AllergenDTO(allergen.Name));
            }

            return Ok(allergensDTO);
        }
    }
}