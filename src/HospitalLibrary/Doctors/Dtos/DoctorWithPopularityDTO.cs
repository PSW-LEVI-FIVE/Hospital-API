using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Managers.Dtos
{
    public class DoctorWithPopularityDTO
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Surname { set; get; }

        public int PatientsPicked { set; get; }
        public DoctorWithPopularityDTO()
        {
        }
        public DoctorWithPopularityDTO(int doctorID, int numberOfPatientsPicked, string doctorName, string doctorSurname)
        {
            Id = doctorID;
            Name = doctorName;
            Surname = doctorSurname;
            PatientsPicked = numberOfPatientsPicked;
        }


    }
}