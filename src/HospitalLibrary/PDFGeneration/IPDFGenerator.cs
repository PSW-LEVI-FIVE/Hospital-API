using System.Collections.Generic;
using HospitalLibrary.Examination;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Patients;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.PDFGeneration
{
    public interface IPDFGenerator
    {
        byte[] GenerateTherapyPdf(Hospitalization hospitalization);
        
        byte[] GenerateExaminationReportPdf(ExaminationReport examinationReport, Patient patient);
    }
}