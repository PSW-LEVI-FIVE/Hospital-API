using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Doctors.Dtos
{
    public class DoctorWithSpecialityDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string  Surname { get; set; }
        [Required]
        public string Uid { get; set; }
        [Required]
        public Speciality Speciality { get; set; }
        
        public DoctorWithSpecialityDTO(Doctor doctor)
        {
            Name = doctor.Name;
            Surname = doctor.Surname;
            Uid = doctor.Uid;
            Speciality = doctor.Speciality;
        }
    }
}