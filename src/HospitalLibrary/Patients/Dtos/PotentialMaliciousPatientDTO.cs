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
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int NumberOfCanceledAppointments { get; set; }

        public PotentialMaliciousPatientDTO(Patient patient)
        {
            Name = patient.Name;
            Surname = patient.Surname;
            NumberOfCanceledAppointments = patient.Appointments.Count;
        }

    }
}
