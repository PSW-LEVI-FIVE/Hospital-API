﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport
{
    public class ExaminationReportStatistics: IExaminationReportStatistics
    {

        private IUnitOfWork _unitOfWork;

        public ExaminationReportStatistics(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SuccessfulUnsuccessfulReportsDto CalculateSuccessfulUnsuccessfulReports()
        {
            int successful = _unitOfWork.ExaminationEventRepository.GetSuccessfulEventsCount();
            int events = _unitOfWork.ExaminationEventRepository.GetAllEventsCount();
            int unsuccessful = events - successful;
            
            return new SuccessfulUnsuccessfulReportsDto(successful, unsuccessful);
        }

        public async Task<List<SpecialtyCountDTO>> CalculateSuccessfulUnsuccessfulPerSpecialty()
        {
            var specialties =  await _unitOfWork.SpecialtyRepository.GetAll();
            var results = new List<SpecialtyCountDTO>();
            foreach (var spec in specialties)
            {
                var reports = _unitOfWork.ExaminationReportRepository
                    .FindAllBySpecialty(spec.Id)
                    .Select(r => r.Id)
                    .ToList();

                var successful = _unitOfWork.ExaminationEventRepository.GetSpecialtySuccessfulCount(reports);
                var events = _unitOfWork.ExaminationEventRepository.GetSpecialtyAllEventsCount(reports);
                var unsuccessful = events - successful;
                results.Add(new SpecialtyCountDTO(spec.Name, successful, unsuccessful));
            }

            return results;
        }

        public AverageStepDTO CalculateStepsAverageTime()
        {

            var symptomAverage = _unitOfWork.ExaminationEventRepository
                .GetAverageTimeForStep(ExaminationReportEventType.STARTED, ExaminationReportEventType.FINISHED_SYMPTOMS);
            var reportAverage = _unitOfWork.ExaminationEventRepository
                .GetAverageTimeForStep(ExaminationReportEventType.FINISHED_SYMPTOMS, ExaminationReportEventType.FINISHED_REPORT);
            var prescriptionAverage = _unitOfWork.ExaminationEventRepository
                .GetAverageTimeForStep(ExaminationReportEventType.FINISHED_REPORT, ExaminationReportEventType.FINISHED_PRESCRPITION);

            return new AverageStepDTO(symptomAverage, reportAverage, prescriptionAverage);
        }

        public Dictionary<int, double> GetAveragePerHour()
        {
            var result =  _unitOfWork.ExaminationEventRepository.GetPerHourAverage();
            var dictionary = result.ToDictionary(el => el.Hour, el => el.Average);
            
            Enumerable.Range(1, 24)
                .ToList()
                .ForEach(el => dictionary.TryAdd(el, 0));
            return dictionary;
        }

        public MinMaxDTO CalculateMinMaxDto()
        {
            var min = _unitOfWork.ExaminationEventRepository.GetMinTime();
            var max = _unitOfWork.ExaminationEventRepository.GetMaxTime();
            var avg = _unitOfWork.ExaminationEventRepository.GetAvgTime();

            return new MinMaxDTO()
            {
                Min = min,
                Max = max,
                Avg = avg
            };
        }
    }
}