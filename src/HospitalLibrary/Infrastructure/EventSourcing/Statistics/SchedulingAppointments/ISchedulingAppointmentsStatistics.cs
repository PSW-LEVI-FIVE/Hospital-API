using HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments.Dtos;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments
{
    public interface ISchedulingAppointmentStatistics
    {
        AveragePatientStepDTO CalculateStepsAverageTime();
        TimesWatchedStepsDTO GetTimesWatchedStep();
        SchedulePerAgeDTO GetAverageTimeForSchedulePerAge(int fromAge, int toAge);
        SchedulePerAgeDTO GetAverageTimeForSchedule();
        AveragePatientStepDTO GetHowManyTimesQuitOnStep();
        TimesWatchedStepsDTO GetLongTermedSteps();
    }
}