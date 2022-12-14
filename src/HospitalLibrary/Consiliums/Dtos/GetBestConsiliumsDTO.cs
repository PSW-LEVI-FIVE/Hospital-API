using System;
using System.Collections.Generic;

namespace HospitalLibrary.Consiliums.Dtos
{
    public class GetBestConsiliumsDTO
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<int> Doctors { get; set; }
        public int SchedulerDoctor { get; set; }
        public int consiliumDuration { get; set; }

        public GetBestConsiliumsDTO(DateTime from, DateTime to, List<int> doctors, int schedulerDoctor, int consiliumDuration)
        {
            From = from;
            To = to;
            Doctors = doctors;
            SchedulerDoctor = schedulerDoctor;
            this.consiliumDuration = consiliumDuration;
        }
    }
}