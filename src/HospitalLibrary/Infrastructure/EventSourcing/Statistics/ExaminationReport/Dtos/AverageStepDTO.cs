using HospitalLibrary.Symptoms;

namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos
{
    public class AverageStepDTO
    {
        public double SymptomAverage { get; set; }
        public double ReportAverage { get; set; }
        public double PrescriptionAverage { get; set; }


        public AverageStepDTO(double symptomAverage, double reportAverage, double prescriptionAverage)
        {
            SymptomAverage = symptomAverage;
            ReportAverage = reportAverage;
            PrescriptionAverage = prescriptionAverage;
        }
    }
}