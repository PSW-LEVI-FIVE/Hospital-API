using HospitalLibrary.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Infrastructure.EventSourcing.Events;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation
{
  public class RenovationStatistics:IRenovationStatistics
  {
    private IUnitOfWork _unitOfWork;

    public RenovationStatistics(IUnitOfWork unitOfWork)
    {
      _unitOfWork=unitOfWork;
    }
    public List<double> GetAvgStepCount()
    {
      double avgStepMerge=_unitOfWork.RenovationStaticsRepository.GetAvgStepCount(RenovationType.MERGE);
      double avgStepSplit = _unitOfWork.RenovationStaticsRepository.GetAvgStepCount(RenovationType.SPLIT);
      return new List<double> {avgStepMerge, avgStepSplit};
    }

    public List<double> GetAverageVisitsToStep(RenovationType type)
    {
      double avgStarted = _unitOfWork.RenovationStaticsRepository.GetAverageVisitsToStep(RenovationEventType.STARTED,type);
      double avgBasicInfo = _unitOfWork.RenovationStaticsRepository.GetAverageVisitsToStep(RenovationEventType.ADDED_BASIC_INFO, type);
      double avgTimeChosen = _unitOfWork.RenovationStaticsRepository.GetAverageVisitsToStep(RenovationEventType.TIME_CHOSEN, type);
      double avgAdditionalInfo = _unitOfWork.RenovationStaticsRepository.GetAverageVisitsToStep(RenovationEventType.ADDED_ADDITION_INFO, type);
      double avgFinished = _unitOfWork.RenovationStaticsRepository.GetAverageVisitsToStep(RenovationEventType.FINISHED, type);
      return new List<double>() { avgStarted, avgBasicInfo, avgTimeChosen, avgAdditionalInfo, avgFinished };
    }

    public List<double> GetAverageTimeForStep(RenovationType type)
    {
      double avgBasicInfo = _unitOfWork.RenovationStaticsRepository
        .GetAverageTimeForStep(RenovationEventType.STARTED, RenovationEventType.ADDED_BASIC_INFO, type);
      double avgTimeChosen = _unitOfWork.RenovationStaticsRepository
        .GetAverageTimeForStep(RenovationEventType.ADDED_BASIC_INFO, RenovationEventType.TIME_CHOSEN, type);
      double avgAdditionalInfo = _unitOfWork.RenovationStaticsRepository
        .GetAverageTimeForStep(RenovationEventType.TIME_CHOSEN, RenovationEventType.ADDED_ADDITION_INFO, type);
      double avgFinished = _unitOfWork.RenovationStaticsRepository
        .GetAverageTimeForStep(RenovationEventType.ADDED_ADDITION_INFO, RenovationEventType.FINISHED, type);
      return new List<double>() { avgBasicInfo, avgTimeChosen, avgAdditionalInfo, avgFinished };

    }

    public List<double> GetAvgTime()
    {
      double avgMerge = _unitOfWork.RenovationStaticsRepository.GetAvgTime(RenovationType.MERGE);
      double avgSplit = _unitOfWork.RenovationStaticsRepository.GetAvgTime(RenovationType.SPLIT);
      return new List<double> { avgMerge, avgSplit };
    }
  }

 
}
