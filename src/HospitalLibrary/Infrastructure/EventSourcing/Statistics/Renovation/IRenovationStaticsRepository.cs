using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation
{
  public interface IRenovationStaticsRepository: IBaseRepository<RenovationDomainEvent>
  {
    double GetAvgStepCount(RenovationType type);
    double GetAverageVisitsToStep(RenovationEventType step, RenovationType type);
    double GetAverageTimeForStep(RenovationEventType stepStart, RenovationEventType stepEnd, RenovationType type);
    double GetAvgTime(RenovationType type);
  }
}
