using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Doctors.Dtos
{
    public class PatientsDoctorDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string  Surname { get; set; }
        [Required]
        public string Uid { get; set; }

        public PatientsDoctorDTO(string name, string surname,string uid)
        {
            Name = name;
            Surname = surname;
            Uid = uid;
        }
    }
}