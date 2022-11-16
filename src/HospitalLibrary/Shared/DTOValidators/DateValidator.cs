using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Shared.DTOValidators
{
    public class DateValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d < DateTime.Now;

        }
    }
}