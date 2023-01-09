using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.BloodOrders;
using HospitalLibrary.BloodOrders.Dtos;
using HospitalLibrary.BloodOrders.Interfaces;
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
            BloodOrder bloodOrder = await _bloodOrderService.Create(bloodOrderDto.MapToModel());
            return Ok(bloodOrder);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ShowBloodOrderDto> bloodOrders = await _bloodOrderService.GetAllBloodOrders();
            return Ok(bloodOrders);
        }
    }
}