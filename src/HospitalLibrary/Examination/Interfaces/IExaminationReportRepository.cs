using System.Collections.Generic;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalLibrary.Examination.Interfaces
{
    public interface IExaminationReportRepository: IBaseRepository<ExaminationReport>
    {
        ExaminationReport GetByExamination(int examinationId);
        List<ExaminationReport> FindAllBySpecialty(int specialtyId);
        ExaminationReport FindExam();
        IEnumerable<ExaminationReport> SearchByPhrase(string phrase);
        IEnumerable<ExaminationReport> SearchByWords(List<string> words);
    }
}