using System;
using System.Linq;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.SchedulingAppointments
{
    public class SchedulingEventRepository : BaseRepository<SchedulingAppointmentDomainEvent> , ISchedulingEventRepository
    {
        public SchedulingEventRepository(HospitalDbContext context) : base(context)
        {
        }
        public double GetAverageTimeForStep(SchedulingAppointmentEventType stepStart, SchedulingAppointmentEventType stepEnd)
        {
            return _dataContext.SchedulingAppointmentDomainEvents
                .Where(d => d.Type == stepStart || d.Type == stepEnd)
                .ToList()
                .GroupBy(d => d.AggregateId)
                .Select(g =>
                    new
                    {
                        Start = g.FirstOrDefault(el => el.Type == stepStart),
                        End = g.FirstOrDefault(el => el.Type == stepEnd)
                    })
                .Where(el => el.Start != null && el.End != null)
                .Select(el => new { Diff = el.End.Timestamp.Subtract(el.Start.Timestamp) })
                .DefaultIfEmpty(new { Diff = new TimeSpan(0, 0, 0) })
                .Average(el => el.Diff.TotalMinutes * 60);
        }

        public double GetAverageTimeForSchedule()
        {
            return _dataContext.SchedulingAppointmentDomainEvents
                .Where(d => d.Type == SchedulingAppointmentEventType.STARTED ||
                            d.Type == SchedulingAppointmentEventType.FINISHED)
                .ToList()
                .GroupBy(d => d.AggregateId)
                .Select(g =>
                    new
                    {
                        Start = g.FirstOrDefault(el => el.Type == SchedulingAppointmentEventType.STARTED),
                        End = g.FirstOrDefault(el => el.Type == SchedulingAppointmentEventType.FINISHED)
                    })
                .Where(el => el.Start != null && el.End != null)
                .Select(el => new { Diff = el.End.Timestamp.Subtract(el.Start.Timestamp) })
                .DefaultIfEmpty(new { Diff = new TimeSpan(0, 0, 0) })
                .Average(el => el.Diff.TotalMinutes * 60);
        }
        
        public int GetTimesWatchedStep(SchedulingAppointmentEventType step)
        {
            return _dataContext.SchedulingAppointmentDomainEvents
                .Where(d => d.Type == step)
                .ToList()
                .Count();
        }
        
        public double GetAverageTimeForSchedulePerAge(DateTime fromAge,DateTime toAge)
        {
            return _dataContext.SchedulingAppointmentDomainEvents
                .Where(d => d.Type == SchedulingAppointmentEventType.STARTED ||
                            d.Type == SchedulingAppointmentEventType.FINISHED)
                .Where(d=>d.Appointment.Patient.BirthDate > fromAge && d.Appointment.Patient.BirthDate < toAge)
                .ToList()
                .GroupBy(d => d.AggregateId)
                .Select(g =>
                    new
                    {
                        Start = g.FirstOrDefault(el => el.Type == SchedulingAppointmentEventType.STARTED),
                        End = g.FirstOrDefault(el => el.Type == SchedulingAppointmentEventType.FINISHED)
                    })
                .Where(el => el.Start != null && el.End != null)
                .Select(el => new { Diff = el.End.Timestamp.Subtract(el.Start.Timestamp) })
                .DefaultIfEmpty(new { Diff = new TimeSpan(0, 0, 0) })
                .Average(el => el.Diff.TotalMinutes  * 60);
        }

        public int GetHowManyTimesQuitOnStep(SchedulingAppointmentEventType step,SchedulingAppointmentEventType nextStep)
        {
            return _dataContext.SchedulingAppointmentDomainEvents
                .Include(d => d.Appointment)
                .Where(d => d.Appointment.State == AppointmentState.NOT_CREATED)
                .ToList()
                .GroupBy(d =>
                    d.AggregateId)
                .Select(g => new
                {
                    Step = g.FirstOrDefault(el => el.Type == step),
                    NextStep = g.FirstOrDefault(el =>el.Type == nextStep)
                })
                .Count(el => el.Step != null && el.NextStep == null);
        }
    }
}