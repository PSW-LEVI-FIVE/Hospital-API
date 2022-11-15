using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Hospitalizations.Dtos
{
    public class EndHospitalizationDTO
    {
        [Required] public DateTime EndDate;
    }
}