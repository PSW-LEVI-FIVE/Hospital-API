using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Consiliums;
using HospitalLibrary.Consiliums.Dtos;
using HospitalLibrary.Consiliums.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/consilium")]
    [ApiController]
    public class ConsiliumController : ControllerBase
    {
        private readonly IConsiliumService _consiliumService;

        public ConsiliumController(IConsiliumService consiliumService)
        {
            _consiliumService = consiliumService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConsiliumDTO createConsiliumDto)
        {
            Appointment newApp = createConsiliumDto.MapToAppointment();
            Consilium consilium = await _consiliumService.Create(newApp, createConsiliumDto.Doctors);
            return Ok(consilium);
        }
    }
}