using System.Threading.Tasks;
using HospitalLibrary.Renovation.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/renovation")]
    [ApiController]
    public class RenovationController : ControllerBase
    {
        private readonly IRenovationService _renovationService;

        public RenovationController(IRenovationService renovationService)
        {
            _renovationService = renovationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPending()
        {
            var pending= await _renovationService.GetAllPending();
            return Ok(pending);
        }
    }
}
