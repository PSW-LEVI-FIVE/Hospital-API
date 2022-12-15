using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Model.ValueObjects;

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
        
        public Reason Reason { get; set; }
        
        public Quantity Quantity { get; set; }

        public BloodOrder()
        {
        }
    }
}