using HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments.Dtos;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments
{
    public interface ISchedulingAppointmentStatistics
    {
        TimesWatchedStepsDTO CalculateStepsAverageTime();
        TimesWatchedStepsDTO GetTimesWatchedStep();
        SchedulePerAgeDTO GetAverageTimeForSchedulePerAge(int fromAge, int toAge);
        SchedulePerAgeDTO GetAverageTimeForSchedule();
        TimesWatchedStepsDTO GetLongTermedSteps();
        TimesWatchedStepsDTO GetHowManyTimesQuitOnStep();
    }
}