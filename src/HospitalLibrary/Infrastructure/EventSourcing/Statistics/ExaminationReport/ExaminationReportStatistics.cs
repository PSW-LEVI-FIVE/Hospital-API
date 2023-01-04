using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Doctors;
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
    }
}