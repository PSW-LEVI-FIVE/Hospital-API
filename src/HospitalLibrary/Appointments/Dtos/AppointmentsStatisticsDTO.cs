using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Appointments.Dtos
{
    public class AppointmentsStatisticsDTO
    {
        public string Date { set; get; }
        public int NumOfAppointments { set; get; }
        public AppointmentsStatisticsDTO(string date, int numOfAppointments)
        {
            this.Date = date;
            this.NumOfAppointments = numOfAppointments;
        }
    }
}
