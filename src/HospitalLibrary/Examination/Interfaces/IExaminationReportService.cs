using System.Threading.Tasks;
using HospitalLibrary.Examination.Dtos;
using HospitalLibrary.Infrastructure.EventSourcing.Events;
using System.Collections.Generic;

namespace HospitalLibrary.Examination.Interfaces
{
    public interface IExaminationReportService
    {
        public Task<ExaminationReportDTO> Create(ExaminationReport report);
        public ExaminationReport GetByExamination(int examinationId);
        public ExaminationReport GetById(int id);
        public Task<ExaminationReport> Update(ExaminationReport report, string uuid);
        public void AddEvent(ExaminationReportDomainEvent examinationReportDomainEvent);
        public Task<IEnumerable<SearchResultDTO>> Search(string phrase);
    }
}