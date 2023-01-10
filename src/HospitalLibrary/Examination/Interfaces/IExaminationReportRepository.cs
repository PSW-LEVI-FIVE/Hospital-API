using System.Collections.Generic;
using HospitalLibrary.Examination.Dtos;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Examination.Interfaces
{
    public interface IExaminationReportRepository: IBaseRepository<ExaminationReport>
    {
        ExaminationReport GetByExamination(int examinationId);
        List<ExaminationReport> FindAllBySpecialty(int specialtyId);
        ExaminationReport FindExam();
        IEnumerable<SearchResultDTO> Search(List<string> word);
    }
}