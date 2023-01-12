using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Appointments;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
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
        public DateTime FindPatientBirth(int aggregateId,List<Appointment> appointments)
        {
            return appointments
                .Where(a => a.Id == aggregateId)
                .Select(a => a.Patient.BirthDate)
                .FirstOrDefault();
        }
        public double GetAverageTimeForSchedulePerAge(DateTime fromAge,DateTime toAge)
        {
            var appointments = _dataContext.Appointments
                .Include(a => a.Patient)
                .ToList();
            return _dataContext.SchedulingAppointmentDomainEvents
                .Where(d => (d.Type == SchedulingAppointmentEventType.STARTED ||
                            d.Type == SchedulingAppointmentEventType.FINISHED))
                .AsEnumerable()
                .Where(d=> Between( FindPatientBirth(d.AggregateId,appointments),fromAge,toAge))
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

        public List<int> FindNotCreated()
        {
            return _dataContext.Appointments
                .Where(a => a.State == AppointmentState.NOT_CREATED)
                .Select(a=>a.Id)
                .ToList();
        }

        public int GetHowManyTimesQuitOnStep(SchedulingAppointmentEventType step,SchedulingAppointmentEventType nextStep)
        {
            return _dataContext.SchedulingAppointmentDomainEvents
                .Where(d => FindNotCreated().Any(id => id == d.AggregateId))
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
        public bool Between (DateTime a, DateTime b, DateTime c)
        {
            return a.CompareTo(b) >= 0 && a.CompareTo(c) <= 0;
        }
        public double GetLongTermedSteps(SchedulingAppointmentEventType stepStart, SchedulingAppointmentEventType stepEnd)
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
                .Where(el => el.End.Timestamp.Subtract(el.Start.Timestamp).TotalSeconds > 30)
                .Select(el => new { Diff = el.End.Timestamp.Subtract(el.Start.Timestamp) })
                .DefaultIfEmpty(new { Diff = new TimeSpan(0, 0, 0) })
                .Average(el => el.Diff.TotalMinutes * 60);
        }
    }
}