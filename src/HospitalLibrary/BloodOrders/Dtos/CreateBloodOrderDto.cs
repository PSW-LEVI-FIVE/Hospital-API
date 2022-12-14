using System;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.BloodOrders.Dtos
{
    public class CreateBloodOrderDto
    {
        public CreateBloodOrderDto(int doctorId, DateTime arrival, BloodType bloodType, string reason, double quantity)
        {
            DoctorId = doctorId;
            Arrival = arrival;
            BloodType = bloodType;
            Reason = reason;
            Quantity = quantity;
        }

        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateTime Arrival { get; set; }
        [Required]
        public BloodType BloodType { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public double Quantity { get; set; }


        public BloodOrder MapToModel()
        {
            return new BloodOrder
            {
                Arrival = Arrival,
                BloodType = BloodType,
                DoctorId = DoctorId,
                OrderDate = DateTime.Today,
                Reason = Reason,
                Quantity = new Quantity(Quantity)
            };
        }
        
        
        
    }
}