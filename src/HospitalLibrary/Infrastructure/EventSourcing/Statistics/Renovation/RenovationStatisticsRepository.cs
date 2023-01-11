using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Settings;
using HospitalLibrary.Shared.Interfaces;
using HospitalLibrary.Shared.Repository;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation
{
  public class RenovationStatisticsRepository:BaseRepository<RenovationDomainEvent>, IRenovationStaticsRepository
  {
    public RenovationStatisticsRepository(HospitalDbContext context) :base(context)
    {
    }

    public double GetAvgStepCount(RenovationType type)
    {
      return _dataContext.RenovationDomainEvents
        .Where(d => d.RenovationType == type)
        .GroupBy(d => d.Uuid)
        .Select(g => g.Count()
        ).ToList().Average();

    }

    public double GetTotalVisitsToStep(RenovationEventType step, RenovationType type)
    {
      return _dataContext.RenovationDomainEvents
        .Where(d => d.RenovationType == type)
        .GroupBy(d => d.Uuid)
        .Select(g => g.Count(el=>el.EventType==step)
        ).ToList().Sum();
    }

    public double GetAverageTimeForStep(RenovationEventType stepStart, RenovationEventType stepEnd, RenovationType type)
    {
      return _dataContext.RenovationDomainEvents
        .Where(d => d.RenovationType == type)
        .Where(d => d.EventType == stepStart || d.EventType == stepEnd)
        .ToList()
        .GroupBy(d => d.Uuid)
        .Select(g =>
          new
          {
            Start = g.FirstOrDefault(el => el.EventType == stepStart),
            End = g.FirstOrDefault(el => el.EventType == stepEnd)
          })
        .Where(el => el.Start != null && el.End != null)
        .Select(el => el.End.Timestamp - el.Start.Timestamp)
        .ToList()
        .Average(el => el.TotalMinutes);
    }

    public double GetMinTimeForStep(RenovationEventType stepStart, RenovationEventType stepEnd, RenovationType type)
    {
      return _dataContext.RenovationDomainEvents
        .Where(d => d.RenovationType == type)
        .Where(d => d.EventType == stepStart || d.EventType == stepEnd)
        .ToList()
        .GroupBy(d => d.Uuid)
        .Select(g =>
          new
          {
            Start = g.FirstOrDefault(el => el.EventType == stepStart),
            End = g.FirstOrDefault(el => el.EventType == stepEnd)
          })
        .Where(el => el.Start != null && el.End != null)
        .Select(el => el.End.Timestamp - el.Start.Timestamp)
        .ToList()
        .Min(el => el.TotalMinutes);
    }

    public double GetMaxTimeForStep(RenovationEventType stepStart, RenovationEventType stepEnd, RenovationType type)
    {
      return _dataContext.RenovationDomainEvents
        .Where(d => d.RenovationType == type)
        .Where(d => d.EventType == stepStart || d.EventType == stepEnd)
        .ToList()
        .GroupBy(d => d.Uuid)
        .Select(g =>
          new
          {
            Start = g.FirstOrDefault(el => el.EventType == stepStart),
            End = g.FirstOrDefault(el => el.EventType == stepEnd)
          })
        .Where(el => el.Start != null && el.End != null)
        .Select(el => el.End.Timestamp - el.Start.Timestamp)
        .ToList()
        .Max(el => el.TotalMinutes);
    }

    public double GetAvgTime(RenovationType type)
    {
      return _dataContext.RenovationDomainEvents
        .Where(e=>e.RenovationType==type)
        .Where(e => e.EventType == RenovationEventType.STARTED ||
                    e.EventType == RenovationEventType.FINISHED)
        .ToList()
        .GroupBy(e => e.Uuid)
        .Select(g =>
          new
          {
            Start = g.FirstOrDefault(el => el.EventType == RenovationEventType.STARTED),
            End = g.FirstOrDefault(el => el.EventType == RenovationEventType.FINISHED)
          })
        .Where(el => el.Start != null && el.End != null)
        .Select(el => el.End.Timestamp - el.Start.Timestamp)
        .ToList()
        .Average(el => el.TotalMinutes);
    }
  }
}
