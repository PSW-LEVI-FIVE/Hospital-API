using System;
using HospitalLibrary.Appointments;
using HospitalLibrary.Doctors.Dtos;

namespace HospitalLibrary.Shared.Dtos
{
    public class TimeIntervalWithDoctorDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public PatientsDoctorDTO DoctorDto { get; set; }
        public TimeIntervalWithDoctorDTO(TimeInterval interval,PatientsDoctorDTO doctorDto)
        {
            this.Start = interval.Start;
            this.End = interval.End;
            this.DoctorDto = doctorDto;
        }
    }
}