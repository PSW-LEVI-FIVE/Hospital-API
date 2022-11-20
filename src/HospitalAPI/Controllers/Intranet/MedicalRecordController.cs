using HospitalLibrary.MedicalRecords;
using HospitalLibrary.MedicalRecords.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [ApiController]
    [Route("api/intranet/medical-records")]
    public class MedicalRecordController: ControllerBase
    {
        private IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }


        [HttpGet]
        [Route("patient/{id:int}")]
        public IActionResult GetOne(int id)
        {
            MedicalRecord record = _medicalRecordService.GetByPatient(id);
            return Ok(record);
        }

    }
}