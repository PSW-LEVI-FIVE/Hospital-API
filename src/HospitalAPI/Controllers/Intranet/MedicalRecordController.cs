using System;
using HospitalLibrary.MedicalRecords;
using HospitalLibrary.MedicalRecords.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles="Doctor")]
        public IActionResult GetOne(int id)
        {
            MedicalRecord record = _medicalRecordService.GetByPatient(id);
            return Ok(record);
        }

        [HttpGet]
        [Route("uid/{uid}")]
        public IActionResult GetOneByUid(string uid)
        {
            Console.WriteLine(uid);
            MedicalRecord record = _medicalRecordService.GetByPatientUid(uid);
            return Ok(record);
        }
    }
}