using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Medicines;
using HospitalLibrary.Medicines.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/medicine")]
    [ApiController]
    public class MedicineController: ControllerBase
    {
     
        private readonly IMedicineService _medicineService;
        
        public MedicineController(IMedicineService bloodStorageService)
        {
            _medicineService = bloodStorageService;
        }
        
        [HttpGet]
        [Route("compatibile/{id}")]
        public  ActionResult GetMedicine(int id)
        {
            IEnumerable<Medicine> bloodConsumption = _medicineService.getAllCompatibileMedicine(id);
            return Ok(bloodConsumption);
        }
    }
}