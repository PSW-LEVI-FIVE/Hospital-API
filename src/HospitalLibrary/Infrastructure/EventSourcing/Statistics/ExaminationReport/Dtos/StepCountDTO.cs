using System;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos
{
    public class StepCountDTO
    {
        public string Name { get; set; }
        public TimeSpan Time { get; set; }

        public StepCountDTO(string name, TimeSpan time)
        {
            Name = name;
            Time = time;
        }
    }
}