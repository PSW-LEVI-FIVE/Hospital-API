using HospitalLibrary.Buildings;
using HospitalLibrary.Buildings.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/buildings")]
    [ApiController]
    public class BuildingsController: ControllerBase
    {
        private IBuildingService _buildingService;

        public BuildingsController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }
        
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles="Manager")]
        public IActionResult UpdateName(int id, [FromBody] string name)
        {
            Building building = _buildingService.GetOne(id);
            building.UpdateName(name);
            _buildingService.Update(building);
            return Ok(building);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles="Manager")]
        public IActionResult GetbyId(int id)
        {
            Building building = _buildingService.GetOne(id);
            return Ok(building);
        }
    }
} 