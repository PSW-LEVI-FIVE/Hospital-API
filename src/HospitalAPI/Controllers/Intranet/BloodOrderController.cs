using System.Threading.Tasks;
using HospitalLibrary.BloodOrders.Dtos;
using HospitalLibrary.BloodOrders.Interfaces;
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
        public async Task<IActionResult> Create([FromBody] CreateBloodOrderDto bloodOrderDto)
        {
            return Ok();
        }
    }
}