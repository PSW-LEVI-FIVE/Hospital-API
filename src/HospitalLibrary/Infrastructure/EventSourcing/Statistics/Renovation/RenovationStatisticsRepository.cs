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
        .Select(g =>
          new
          {
            Start = g.FirstOrDefault(el => el.EventType == RenovationEventType.STARTED),
            End = g.FirstOrDefault(el => el.EventType == RenovationEventType.FINISHED),
            count = g.Count()
          })
        .Where(el => el.Start != null && el.End != null)
        .Average(el => el.count);

    }

    public double GetAverageVisitsToStep(RenovationEventType step, RenovationType type)
    {
      return _dataContext.RenovationDomainEvents
        .Where(d => d.RenovationType == type)
        .GroupBy(d => d.Uuid)
        .Select(g =>
          new
          {
            Start = g.FirstOrDefault(el => el.EventType == RenovationEventType.STARTED),
            End = g.FirstOrDefault(el => el.EventType == RenovationEventType.FINISHED),
            step = g.Select(el => el.EventType == step),
          })
        .Where(el => el.Start != null && el.End != null)
        .Average(el => el.step.Count());
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
