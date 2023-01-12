using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalLibrary.BloodOrders;
using HospitalLibrary.BloodOrders.Dtos;
using HospitalLibrary.BloodOrders.Interfaces;
using HospitalLibrary.Users;
using HospitalLibrary.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/blood-orders")]
    [ApiController]
    public class BloodOrderController:ControllerBase
    {
        private readonly IBloodOrderService _bloodOrderService;

        public BloodOrderController(IBloodOrderService bloodOrderService)
        {
            _bloodOrderService = bloodOrderService;
        }

        [HttpPost]
        [Authorize(Roles="Doctor")]
        public async Task<IActionResult> Create([FromBody] CreateBloodOrderDto bloodOrderDto)
        {
            bloodOrderDto.DoctorId = GetCurrentUser().Id;
            BloodOrder bloodOrder = await _bloodOrderService.Create(bloodOrderDto.MapToModel());
            return Ok(bloodOrder);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ShowBloodOrderDto> bloodOrders = await _bloodOrderService.GetAllBloodOrders();
            return Ok(bloodOrders);
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