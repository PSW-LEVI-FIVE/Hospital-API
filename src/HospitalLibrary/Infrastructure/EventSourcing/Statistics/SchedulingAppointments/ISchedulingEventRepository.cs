using System;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments
{
    public interface ISchedulingEventRepository: IBaseRepository<SchedulingAppointmentDomainEvent>
    {
        double GetAverageTimeForStep(SchedulingAppointmentEventType stepStart, SchedulingAppointmentEventType stepEnd);
        double GetAverageTimeForSchedule();
        int GetTimesWatchedStep(SchedulingAppointmentEventType step);
        double GetAverageTimeForSchedulePerAge(DateTime fromAge, DateTime toAge);
        int GetHowManyTimesQuitOnStep(SchedulingAppointmentEventType step, SchedulingAppointmentEventType nextStep);
    }
}