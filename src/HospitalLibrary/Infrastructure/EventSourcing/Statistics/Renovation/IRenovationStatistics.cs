using HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation
{
  public interface IRenovationStatistics
  {
    AvgStepCountDto GetAvgStepCount();
    TotalStepVisitDto GetTotalVisitsToStep(RenovationType type);
    AvgStepTimeDto GetAverageTimeForStep(RenovationType type);
    AvgStepTimeDto GetMinTimeForStep(RenovationType type);
    AvgStepTimeDto GetMaxTimeForStep(RenovationType type);
    AvgTimeDto GetAvgTime();

  }
}
