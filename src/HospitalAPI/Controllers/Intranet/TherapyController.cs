using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.Therapies.Dtos;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Therapies.Model;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/therapies")]
    [ApiController]
    public class TherapyController: ControllerBase
    {
        private readonly ITherapyService _therapyService;
        
        public TherapyController(ITherapyService therapyService)
        {
            _therapyService = therapyService;
        }
        
        [Route("blood")]
        [HttpPost]
        [Authorize(Roles="Doctor")]
        public async Task<IActionResult> CreateBloodTherapy([FromBody] CreateBloodTherapyDTO createBloodTherapyDto)
        {
            createBloodTherapyDto.DoctorId=GetCurrentUser().Id;
            BloodTherapy bloodTherapy = await _therapyService.CreateBloodTherapy(createBloodTherapyDto.MapToModel());
            return Ok(bloodTherapy);
        }
        
        [Route("medicine")]
        [HttpPost]
        [Authorize(Roles="Doctor")]
        public IActionResult CreateMedicineTherapy([FromBody] CreateMedicineTherapyDTO createMedicineTherapyDto)
        {   
            createMedicineTherapyDto.DoctorId=GetCurrentUser().Id;
            MedicineTherapy medicineTherapy = _therapyService.CreateMedicineTherapy(createMedicineTherapyDto.MapToModel());
            return Ok(medicineTherapy);
        }
        
        [Route("blood-consumption")]
        [HttpGet]
        [Authorize(Roles="Doctor")]
        public IActionResult GetBloodConsumption()
        {
            List<BloodConsumptionDTO> bloodConsumption = _therapyService.GetBloodConsumption();
            return Ok(bloodConsumption);
        }
        
        [Route("hospitalization/"+"{id}")]
        [HttpGet]
        [Authorize(Roles="Doctor")]
        public IActionResult GetAllHospitalizationTherapies(int id)
        {
            List<HospitalizationTherapiesDTO> therapies = _therapyService.GetAllHospitalizationTherapies(id);
            return Ok(therapies);
        }
        
        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserDTO
                {
                    Id = int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    Role = Role.Doctor,
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
                };
            }

            return null;
        }
    }
}