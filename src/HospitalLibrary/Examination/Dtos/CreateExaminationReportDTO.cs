using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HospitalLibrary.Appointments;
using HospitalLibrary.Shared.Exceptions;
using HospitalLibrary.Symptoms;

namespace HospitalLibrary.Examination.Dtos
{
    public class CreateExaminationReportDTO
    {
        public int? Id { get; set; }
        public string? Content { get; set; }
        public int DoctorId { get; set; }
        public List<Prescription>? Prescriptions { get; set; }
        public List<Symptom>? Symptoms { get; set; }
        public int ExaminationId { get; set; }



        public ExaminationReport MapToModel()
        {
            return new ExaminationReport(Id ?? 0, DoctorId, Prescriptions, Symptoms, ExaminationId, Content);
        }

        public ExaminationReport MapInitialToModel()
        {
            return new ExaminationReport(DoctorId, ExaminationId);
        }
    }
}