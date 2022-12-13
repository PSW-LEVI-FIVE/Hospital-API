using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.AnnualLeaves.Dtos
{
    public class MonthlyLeavesDTO
    {
        public string Month { set; get; }
        public int TakenDays { set; get; }
        public MonthlyLeavesDTO(DateTime month, int takenDays)
        {
            this.Month = month.ToString("MMMM");
            this.TakenDays = takenDays;
        }
    }
}
