using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Doctors;

namespace HospitalLibrary.BloodOrders
{
    public class BloodOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key()]
        public int Id { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        
        public DateTime Arrival { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public BloodType BloodType { get; set; }
        
        public string Reason { get; set; }
        
        public double Quantity { get; set; }

        public BloodOrder()
        {
        }
    }
}