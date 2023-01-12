namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments.Dtos
{
    public class TimesWatchedStepsDTO
    {
        public double DateStep { get; set; }
        public double SpecialityStep { get; set; }
        public double DoctorStep { get; set; }
        public double TimeStep { get; set; }
        public double FinishedStep { get; set; }


        public TimesWatchedStepsDTO(double dateStep, double specialityStep,double doctorStep, double timeStep, double finishedStep)
        {
            DateStep = dateStep;
            SpecialityStep = specialityStep;
            DoctorStep = doctorStep;
            TimeStep = timeStep;
            FinishedStep = finishedStep;
        }
    }
}