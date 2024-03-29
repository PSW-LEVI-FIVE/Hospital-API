﻿using System;
using System.ComponentModel.DataAnnotations;
using HospitalLibrary.BloodStorages;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.Therapies.Dtos
{
    public class CreateBloodTherapyDTO
    {
        [Required]
        public int  HospitalizationId { get; set; }
        [Required]        
        public DateTime GivenAt { get; set;  }
        [Required]
        public int Type { get; set; }
        [Required]
        public double  Quantity { get; set; }
        [Required]
        public int DoctorId { get; set; }


        public BloodTherapy MapToModel()
        {
            return new BloodTherapy(HospitalizationId, GivenAt, (BloodType)Type, Quantity, DoctorId);
        }
    }
}