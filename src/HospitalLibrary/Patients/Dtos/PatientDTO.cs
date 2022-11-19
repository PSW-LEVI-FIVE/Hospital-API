using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Patients.Dtos
{
    public class PatientDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        
        public PatientDTO(CreatePatientDTO createPatientDto)
        {
            Name = createPatientDto.Name;
            Surname = createPatientDto.Surname;
            Email = createPatientDto.Email;
        }
    }
    
}