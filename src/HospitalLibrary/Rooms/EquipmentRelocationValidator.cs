using System;
using System.Threading.Tasks;
using HospitalLibrary.Rooms.Interfaces;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Shared.Exceptions;


namespace HospitalLibrary.Rooms
{
    public class EquipmentRelocationValidator : IEquipmenrRelocationValidator
    {
        public EquipmentRelocationValidator()
        {
            
        }
        
        public async Task Validate(EquipmentReallocation equipmentReallocation)
        {
          ThrowIfLessThan24hours(equipmentReallocation);
        }

        public void ThrowIfLessThan24hours(EquipmentReallocation equipmentReallocation)
        {
        var time = equipmentReallocation.StartAt;
            DateTime now = DateTime.Now;
                if (((time-now).TotalHours)<=24)
            {
                throw  new BadRequestException(
                    "You cant cancel relocation less than 24 hours before it starts");
            }
        }
    }
}