namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos
{
    public class PerHourAverageDTO
    {
        public int Hour { get; set; }
        public double Average { get; set; }

        public PerHourAverageDTO(int hour, double average)
        {
            Hour = hour;
            Average = average;
        }
    }
}