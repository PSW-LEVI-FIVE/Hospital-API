using System;
using System.Collections.Generic;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Model.ValueObjects;

namespace HospitalLibrary.Consiliums.Dtos
{
    public class CreateConsiliumDTO
    {
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }
        
        public int RoomId { get; set; }

        public string Title { get; set; }
        
        public List<int> Doctors { get; set; }

        public Appointment MapToAppointment()
        {
            return new Appointment
            {
                RoomId = RoomId,
                StartAt = Start,
                EndAt = End,
                State = AppointmentState.PENDING,
                Type = AppointmentType.CONSILIUM,
                Consilium = new Consilium(Title)
            };
        }
    }
}