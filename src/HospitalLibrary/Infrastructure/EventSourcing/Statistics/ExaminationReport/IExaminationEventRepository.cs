using System.Collections.Generic;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport
{
    public interface IExaminationEventRepository: IBaseRepository<ExaminationReportDomainEvent>
    {
        int GetSuccessfulEventsCount();
        int GetAllEventsCount();

        int GetSpecialtySuccessfulCount(List<int> reportIds);
        int GetSpecialtyAllEventsCount(List<int> reportIds);

        double GetAverageTimeForStep(ExaminationReportEventType stepStart, ExaminationReportEventType stepEnd);

        List<PerHourAverageDTO> GetPerHourAverage();

    }
}