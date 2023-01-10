using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation.DTO
{
  public class AvgStepTimeDto
  {
    public double BasicInfo;
    public double TimeChosen;
    public double AdditionInfo;
    public double Finished;

    public AvgStepTimeDto(double basicInfo, double timeChosen, double additionInfo, double finished)
    {
      BasicInfo = basicInfo;
      TimeChosen = timeChosen;
      AdditionInfo = additionInfo;
      Finished = finished;
    }
  }
}
