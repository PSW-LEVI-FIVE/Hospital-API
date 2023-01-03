using System;
using System.Collections.Generic;
using HospitalLibrary.Symptoms;
using Npgsql.TypeHandlers;

namespace HospitalLibrary.Examination.Dtos
{
    public class ExaminationReportDTO
    {

        public int Id { get; set; }
        
        public int DoctorId { get; private set; }

        public string? Content { get; private set; }
        
        public int ExaminationId { get; private set; }
        
        public List<Prescription> Prescriptions { get; private set; }
        public List<Symptom> Symptoms { get; private set; }
        
        public string? Url { get; private set; }

        public string Uuid { get; set; }

        public ExaminationReportDTO(ExaminationReport examinationReport, string uuid)
        {
            Id = examinationReport.Id;
            DoctorId = examinationReport.DoctorId;
            Content = examinationReport.Content;
            ExaminationId = examinationReport.ExaminationId;
            Prescriptions = examinationReport.Prescriptions;
            Symptoms = examinationReport.Symptoms;
            Uuid = uuid;
        }
    }
}