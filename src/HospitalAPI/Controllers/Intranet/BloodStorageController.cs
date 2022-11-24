using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.BloodStorages.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/blood-storage")]
    [ApiController]
    public class BloodStorageController: ControllerBase
    {
        
        private readonly IBloodStorageService _bloodStorageService;
        
        public BloodStorageController(IBloodStorageService bloodStorageService)
        {
            _bloodStorageService = bloodStorageService;
        }
        
        [HttpGet]
        [Route("compatibile/{id}")]
        public ActionResult GetBloodStorage(int id)
        {
            List<BloodType> bloodConsumption = _bloodStorageService.GetAllCompatibileBloodStorage(id);
            return Ok(bloodConsumption);
        }
    }
}