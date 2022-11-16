using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Hospitalizations.Dtos
{
    public class EndHospitalizationDTO
    {
        [Required] 
        public DateTime EndTime { get; set; }

        public EndHospitalizationDTO(DateTime endTime)
        {
            EndTime = endTime;
        }
    }
}