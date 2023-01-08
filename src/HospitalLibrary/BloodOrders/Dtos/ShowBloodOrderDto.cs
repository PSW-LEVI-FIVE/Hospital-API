using System;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Doctors;

namespace HospitalLibrary.BloodOrders.Dtos
{
    public class ShowBloodOrderDto
    {
        [Required] public string DoctorFullName { get; set; }
        [Required] public DateTime OrderDate { get; set; }
        [Required] public string BloodType { get; set; }
        [Required] public string Reason { get; set; }
        [Required] public double Quantity { get; set; }

        public ShowBloodOrderDto(BloodOrder order, Doctor doctor)
        {
            DoctorFullName = doctor.Name +" "+ doctor.Surname;
            OrderDate = order.OrderDate;
            BloodType = GetBloodTypeString((int)order.BloodType);
            Reason = order.Reason.Text;
            Quantity = order.Quantity.Count;
        }

        private string GetBloodTypeString(int bType)
        {
            string blood = "";
            switch (bType)
            {
                case 0:
                {
                    blood = "A positive";
                    break;
                }
                case 1:
                {
                    blood = "A negative";
                    break;
                }
                case 2:
                {
                    blood = "B positive";
                    break;
                }
                case 3:
                {
                    blood = "B negative";
                    break;
                }
                case 4:
                {
                    blood = "AB positive";
                    break;
                }
                case 5:
                {
                    blood = "AB negative";
                    break;
                }
                case 6:
                {
                    blood = "0 positive";
                    break;
                }
                case 7:
                {
                    blood = "0 negative";
                    break;
                }
            }
            return blood;
        }
    }
}