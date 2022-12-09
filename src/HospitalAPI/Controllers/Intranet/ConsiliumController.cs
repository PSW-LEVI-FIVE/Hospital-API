using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Consiliums;
using HospitalLibrary.Consiliums.Dtos;
using HospitalLibrary.Consiliums.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
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

        [HttpPost]
        [Route("suggest")]
        public IActionResult GetBestConsiliums([FromBody] GetBestConsiliumsDTO bestConsiliumsDto)
        {
            Console.WriteLine("USO");
            bestConsiliumsDto.SchedulerDoctor = GetCurrentUser().Id;
            bestConsiliumsDto.Doctors.Add(GetCurrentUser().Id);
            TimeInterval timeInterval = new TimeInterval(bestConsiliumsDto.From, bestConsiliumsDto.To);
            GetBestConsiliumsDTO suggestedConsiliums =
                _consiliumService.SuggestConsilium(timeInterval, bestConsiliumsDto.Doctors, bestConsiliumsDto.SchedulerDoctor, bestConsiliumsDto.consiliumDuration);
            return Ok(suggestedConsiliums);
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