using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport
{
    public interface IExaminationReportStatistics
    {

        SuccessfulUnsuccessfulReportsDto CalculateSuccessfulUnsuccessfulReports();
        Task<List<SpecialtyCountDTO>> CalculateSuccessfulUnsuccessfulPerSpecialty();
        AverageStepDTO CalculateStepsAverageTime();
        MinMaxDTO CalculateMinMaxDto();
        Dictionary<int, double> GetAveragePerHour();
    }
}