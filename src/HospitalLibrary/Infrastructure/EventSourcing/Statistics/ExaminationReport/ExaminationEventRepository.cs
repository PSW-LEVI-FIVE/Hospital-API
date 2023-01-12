

using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Repository;
using NUnit.Framework;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport
{
    public class ExaminationEventRepository : BaseRepository<ExaminationReportDomainEvent>, IExaminationEventRepository
    {
        public ExaminationEventRepository(HospitalDbContext context) : base(context)
        {
        }

        public int GetSuccessfulEventsCount()
        {
            return _dataContext.ExaminationReportDomainEvents
                .Count(e => e.Type == ExaminationReportEventType.FINISHED);
        }

        public int GetAllEventsCount()
        {
            return _dataContext.ExaminationReportDomainEvents
                .Count(e => e.Type == ExaminationReportEventType.STARTED);
        }

        public int GetSpecialtySuccessfulCount(List<int> reportIds)
        {
            return _dataContext.ExaminationReportDomainEvents
                .Where(e => reportIds.Any(r => r == e.AggregateId))
                .Count(e => e.Type == ExaminationReportEventType.FINISHED);
        }

        public int GetSpecialtyAllEventsCount(List<int> reportIds)
        {
            return _dataContext.ExaminationReportDomainEvents
                .Count(e => reportIds.Any(r => r == e.AggregateId) && e.Type == ExaminationReportEventType.STARTED);
        }

        public double GetAverageTimeForStep(ExaminationReportEventType stepStart, ExaminationReportEventType stepEnd)
        {
            return _dataContext.ExaminationReportDomainEvents
                .Where(d => d.Type == stepStart || d.Type == stepEnd)
                .ToList()
                .GroupBy(d => d.Uuid)
                .Select(g =>
                    new
                    {
                        Start = g.FirstOrDefault(el => el.Type == stepStart),
                        End = g.FirstOrDefault(el => el.Type == stepEnd)
                    })
                .Where(el => el.Start != null && el.End != null)
                .Select(el => new { Diff = el.End.Timestamp.Subtract(el.Start.Timestamp) })
                .DefaultIfEmpty(new { Diff = new TimeSpan(0, 0, 0) })
                .Average(el => el.Diff.TotalMinutes);
        }

        public List<PerHourAverageDTO> GetPerHourAverage()
        {
            return _dataContext.ExaminationReportDomainEvents
                .Where(d => d.Type == ExaminationReportEventType.STARTED || d.Type == ExaminationReportEventType.FINISHED)
                .ToList()
                .GroupBy(d => d.Uuid)
                .Select(g =>
                    new
                    {
                        Start = g.FirstOrDefault(el => el.Type == ExaminationReportEventType.STARTED),
                        End = g.FirstOrDefault(el => el.Type == ExaminationReportEventType.FINISHED)
                    })
                .Where(el => el.Start != null && el.End != null)
                .Select(el => new { Hour = el.Start.Timestamp.Hour, Diff = el.End.Timestamp.Subtract(el.Start.Timestamp).TotalMinutes })
                .ToList()
                .GroupBy(el => el.Hour)
                .Select(g => new PerHourAverageDTO(g.Key, g.Average(el => el.Diff)))
                .ToList();
        }

        public double GetMinTime()
        {
            return _dataContext.ExaminationReportDomainEvents
                .Where(e => e.Type == ExaminationReportEventType.STARTED ||
                            e.Type == ExaminationReportEventType.FINISHED)
                .ToList()
                .GroupBy(e => e.Uuid)
                .Select(g =>
                    new
                    {
                        Start = g.FirstOrDefault(el => el.Type == ExaminationReportEventType.STARTED),
                        End = g.FirstOrDefault(el => el.Type == ExaminationReportEventType.FINISHED)
                    })
                .Where(el => el.Start != null && el.End != null)
                .Select(el => new {Diff = el.End.Timestamp - el.Start.Timestamp })
                .DefaultIfEmpty(new { Diff = new TimeSpan(0, 0, 0) })
                .ToList()
                .Min(el => el.Diff)
                .TotalMinutes;
        }

        public double GetMaxTime()
        {
            return _dataContext.ExaminationReportDomainEvents
                .Where(e => e.Type == ExaminationReportEventType.STARTED ||
                            e.Type == ExaminationReportEventType.FINISHED)
                .ToList()
                .GroupBy(e => e.Uuid)
                .Select(g =>
                    new
                    {
                        Start = g.FirstOrDefault(el => el.Type == ExaminationReportEventType.STARTED),
                        End = g.FirstOrDefault(el => el.Type == ExaminationReportEventType.FINISHED)
                    })
                .Where(el => el.Start != null && el.End != null)
                .Select(el => new {Diff = el.End.Timestamp - el.Start.Timestamp })
                .DefaultIfEmpty(new { Diff = new TimeSpan(0, 0, 0) })
                .Max(el => el.Diff)
                .TotalMinutes;
        }

        public double GetAvgTime()
        {
            return _dataContext.ExaminationReportDomainEvents
                .Where(e => e.Type == ExaminationReportEventType.STARTED ||
                            e.Type == ExaminationReportEventType.FINISHED)
                .ToList()
                .GroupBy(e => e.Uuid)
                .Select(g =>
                    new
                    {
                        Start = g.FirstOrDefault(el => el.Type == ExaminationReportEventType.STARTED),
                        End = g.FirstOrDefault(el => el.Type == ExaminationReportEventType.FINISHED)
                    })
                .Where(el => el.Start != null && el.End != null)
                .Select(el => new {Diff = el.End.Timestamp - el.Start.Timestamp })
                .DefaultIfEmpty(new { Diff = new TimeSpan(0, 0, 0) })
                .ToList()
                .Average(el => el.Diff.TotalMinutes);
        }
    }
}