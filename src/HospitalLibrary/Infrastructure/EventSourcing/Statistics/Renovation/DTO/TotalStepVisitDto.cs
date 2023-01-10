using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation.DTO
{
  public class TotalStepVisitDto
  {
    public double Start;
    public double BasicInfo;
    public double TimeChosen;
    public double AdditionInfo;
    public double Finished;
    public double Canceled;
    public TotalStepVisitDto(double start, double basicInfo, double timeChosen, double additionInfo, double finished, double canceled)
    {
      Start = start;
      BasicInfo = basicInfo;
      TimeChosen = timeChosen;
      AdditionInfo = additionInfo;
      Finished = finished;
      Canceled = canceled;
    }
  }
}
