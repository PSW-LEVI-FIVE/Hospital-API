using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Managers.Dtos
{
    public class DoctorsPopularityDTO
    {
        public int DoctorID { set; get; }

        public string DoctorName { set; get; }

        public string DoctorSurname { set; get; }

        public int NumberOfPatientsPicked { set; get; }

        DoctorsPopularityDTO()
        {

        }
        DoctorsPopularityDTO(int doctorID, int numberOfPatientsPicked)
        {
            DoctorID = doctorID;
            NumberOfPatientsPicked = numberOfPatientsPicked;
        }
        

    }
}
