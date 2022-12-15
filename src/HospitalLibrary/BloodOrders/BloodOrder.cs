using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Model;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.BloodOrders
{
    public class BloodOrder: BaseEntity
    {


        [ForeignKey("Doctor")]
        public int DoctorId { get; private set; }
        public Doctor Doctor { get; private set; }
        public DateTime Arrival { get; private set; }
        public DateTime OrderDate { get; private set; }
        public BloodType BloodType { get; private set; }
        public Reason Reason { get; private set; }
        public Quantity Quantity { get; private set; }
        
        public BloodOrder(int doctorId, DateTime arrival, DateTime orderDate, BloodType bloodType, Reason reason, Quantity quantity)
        {
            DoctorId = doctorId;
            Arrival = arrival;
            OrderDate = orderDate;
            BloodType = bloodType;
            Reason = reason;
            Quantity = quantity;
        }
        
    }
}