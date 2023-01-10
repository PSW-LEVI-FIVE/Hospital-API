using HospitalLibrary.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation.DTO;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.Renovation
{
  public class RenovationStatistics:IRenovationStatistics
  {
    private IUnitOfWork _unitOfWork;

    public RenovationStatistics(IUnitOfWork unitOfWork)
    {
      _unitOfWork=unitOfWork;
    }
    public AvgStepCountDto GetAvgStepCount()
    {
      double avgStepMerge=_unitOfWork.RenovationStaticsRepository.GetAvgStepCount(RenovationType.MERGE);
      double avgStepSplit = _unitOfWork.RenovationStaticsRepository.GetAvgStepCount(RenovationType.SPLIT);
      return new AvgStepCountDto(avgStepMerge, avgStepSplit);
    }

    public TotalStepVisitDto GetTotalVisitsToStep(RenovationType type)
    {
      double Started = _unitOfWork.RenovationStaticsRepository.GetTotalVisitsToStep(RenovationEventType.STARTED,type);
      double BasicInfo = _unitOfWork.RenovationStaticsRepository.GetTotalVisitsToStep(RenovationEventType.ADDED_BASIC_INFO, type);
      double TimeChosen = _unitOfWork.RenovationStaticsRepository.GetTotalVisitsToStep(RenovationEventType.TIME_CHOSEN, type);
      double AdditionalInfo = _unitOfWork.RenovationStaticsRepository.GetTotalVisitsToStep(RenovationEventType.ADDED_ADDITION_INFO, type);
      double Finished = _unitOfWork.RenovationStaticsRepository.GetTotalVisitsToStep(RenovationEventType.FINISHED, type);
      double Canceled = _unitOfWork.RenovationStaticsRepository.GetTotalVisitsToStep(RenovationEventType.CANCELED, type);
      return new TotalStepVisitDto( Started, BasicInfo, TimeChosen, AdditionalInfo, Finished,Canceled );
    }

    public AvgStepTimeDto GetAverageTimeForStep(RenovationType type)
    {
      double avgBasicInfo = _unitOfWork.RenovationStaticsRepository
        .GetAverageTimeForStep(RenovationEventType.STARTED, RenovationEventType.ADDED_BASIC_INFO, type);
      double avgTimeChosen = _unitOfWork.RenovationStaticsRepository
        .GetAverageTimeForStep(RenovationEventType.ADDED_BASIC_INFO, RenovationEventType.TIME_CHOSEN, type);
      double avgAdditionalInfo = _unitOfWork.RenovationStaticsRepository
        .GetAverageTimeForStep(RenovationEventType.TIME_CHOSEN, RenovationEventType.ADDED_ADDITION_INFO, type);
      double avgFinished = _unitOfWork.RenovationStaticsRepository
        .GetAverageTimeForStep(RenovationEventType.ADDED_ADDITION_INFO, RenovationEventType.FINISHED, type);
      return new AvgStepTimeDto( avgBasicInfo, avgTimeChosen, avgAdditionalInfo, avgFinished );

    }

    public AvgTimeDto GetAvgTime()
    {
      double avgMerge = _unitOfWork.RenovationStaticsRepository.GetAvgTime(RenovationType.MERGE);
      double avgSplit = _unitOfWork.RenovationStaticsRepository.GetAvgTime(RenovationType.SPLIT);
      return new AvgTimeDto( avgMerge, avgSplit );
    }
  }

 
}
