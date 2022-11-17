using System.Collections.Generic;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Patients;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.PDFGeneration
{
    public interface IPDFGenerator
    {
        byte[] GenerateTherapyPdf(Hospitalization hospitalization, Patient patient);
    }
}