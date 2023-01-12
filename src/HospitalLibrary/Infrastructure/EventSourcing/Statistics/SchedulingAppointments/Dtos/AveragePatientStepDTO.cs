namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments.Dtos
{
    public class AveragePatientStepDTO
    {
        public double DateAverage { get; set; }
        public double SpecialityAverage { get; set; }
        public double DoctorAverage { get; set; }
        public double TimeAverage { get; set; }
        public double ScheduleAverage { get; set; }


        public AveragePatientStepDTO(double dateAverage, double specialityAverage, double doctorAverage,double timeAverage, double scheduleAverage)
        {
            DateAverage = dateAverage;
            SpecialityAverage = specialityAverage;
            DoctorAverage = doctorAverage;
            TimeAverage = timeAverage;
            ScheduleAverage = scheduleAverage;
        }
    }
}