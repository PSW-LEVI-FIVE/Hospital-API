using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation.DTO
{
  public class AvgTimeDto
  {
    public double Merge { get; set; }
    public double Split { get; set; }

    public AvgTimeDto(double avgTimeMerge, double avgTimeSplit)
    {
      Merge = avgTimeMerge;
      Split = avgTimeSplit;
    }
  }
}
