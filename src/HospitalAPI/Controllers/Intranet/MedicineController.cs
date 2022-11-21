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
        public async Task<IActionResult> GetMedicine()
        {
            IEnumerable<Medicine> bloodConsumption = await _medicineService.getAllMedicine();
            return Ok(bloodConsumption);
        }
    }
}