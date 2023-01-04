using System;
using System.Threading.Tasks;
using HospitalLibrary.Renovations.Interface;
using HospitalLibrary.Shared.Exceptions;

namespace HospitalLibrary.Renovations
{
    public class RenovationValidator: IRenovationValidator
    {
        public RenovationValidator()
        {
        }

        public async Task Validate(Renovations.Model.Renovation renovation)
        {
            ThrowIfLessThan24hours(renovation);
        }

        public void ThrowIfLessThan24hours(Renovations.Model.Renovation renovation)
        {
            var time = renovation.StartAt;
            DateTime now = DateTime.Now;
            if (((time-now).TotalHours)<=24)
            {
                throw  new BadRequestException(
                    "You cant cancel renovation less than 24 hours before it starts");
            }
        }

        
    }
}