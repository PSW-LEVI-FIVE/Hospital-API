using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation
{
  public interface IRenovationStatistics
  {
    List<double> GetAvgStepCount();
    List<double> GetTotalVisitsToStep(RenovationType type);
    List<double> GetAverageTimeForStep(RenovationType type);
    List<double> GetAvgTime();

  }
}
