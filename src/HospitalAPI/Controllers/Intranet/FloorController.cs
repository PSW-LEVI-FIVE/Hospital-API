using HospitalLibrary.Floors;
using HospitalLibrary.Floors.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/floors")]
    [ApiController]
    public class FloorController: ControllerBase
    {
        private IFloorService _floorService;
        public FloorController(IFloorService floorService) { _floorService = floorService; }
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles="Manager")]
        public IActionResult GetbyId(int id)
        {
            Floor floor = _floorService.GetOne(id);
            return Ok(floor);
        }

    }
}
