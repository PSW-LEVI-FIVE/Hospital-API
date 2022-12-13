using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Patients.Dtos
{
    public class PotentialMaliciousPatientDTO
    {
        [Required]
        public string Patient { get; set; }
        [Required]
        public int NumberOfCanceledAppointments { get; set; }
        [Required]
        public Boolean Blocked { get; set; }
        public int Id { get; set; }
        public PotentialMaliciousPatientDTO(Patient patient, bool blocked = false)
        {
            Id = patient.Id;
            Patient = patient.Id + " " + patient.Name + " " + patient.Surname;
            NumberOfCanceledAppointments = patient.Appointments.Count;
            Blocked = blocked;
        }

    }
}
