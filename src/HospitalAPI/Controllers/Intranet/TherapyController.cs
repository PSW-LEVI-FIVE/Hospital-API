using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Therapies.Dtos;
using HospitalLibrary.Therapies.Interfaces;
using HospitalLibrary.Therapies.Model;
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
        public async Task<IActionResult> CreateBloodTherapy([FromBody] CreateBloodTherapyDTO createBloodTherapyDto)
        {
            BloodTherapy bloodTherapy = await _therapyService.CreateBloodTherapy(createBloodTherapyDto.MapToModel());
            return Ok(bloodTherapy);
        }
        
        [Route("medicine")]
        [HttpPost]
        public IActionResult CreateMedicineTherapy([FromBody] CreateMedicineTherapyDTO createMedicineTherapyDto)
        {
            MedicineTherapy medicineTherapy = _therapyService.CreateMedicineTherapy(createMedicineTherapyDto.MapToModel());
            return Ok(medicineTherapy);
        }
        
        [Route("blood-consumption")]
        [HttpGet]
        public IActionResult GetBloodConsumption()
        {
            List<BloodTherapy> bloodConsumption = _therapyService.GetBloodConsumption();
            return Ok(bloodConsumption);
        }
    }
}