using HospitalLibrary.Doctors;
using HospitalLibrary.Managers.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using HospitalLibrary.Allergens;
using HospitalLibrary.Allergens.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/allergen")]
    [ApiController]
    public class AllergenController : ControllerBase
    {
        private IAllergenService _allergenService;

        public AllergenController(IAllergenService allergenService)
        {
            _allergenService = allergenService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("statistics/AllergensWithPatients")]
        public async Task<IActionResult> GetAllergensWithNumberOfPatients()
        {
            Task<IEnumerable<Allergen>> allergens = _allergenService.GetAllergensWithNumberOfPatients();
            var allergensWithNumberOfPatients = new List<AllergenWithNumberPatientsDTO>();
            foreach (Allergen allergen in allergens.Result)
                allergensWithNumberOfPatients.Add(new AllergenWithNumberPatientsDTO(allergen.Name, allergen.Patients.Count));
              
            return Ok(allergensWithNumberOfPatients.AsEnumerable());
        }
    }
}
