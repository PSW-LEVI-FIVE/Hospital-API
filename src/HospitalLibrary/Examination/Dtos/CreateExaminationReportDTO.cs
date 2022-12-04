using System.Collections.Generic;
using HospitalLibrary.Appointments;
using HospitalLibrary.Symptoms;

namespace HospitalLibrary.Examination.Dtos
{
    public class CreateExaminationReportDTO
    {
        public string Content { get; set; }
        public int DoctorId { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<Symptom> Symptoms { get; set; }
        public Appointment Examination { get; set; }



        public ExaminationReport MapToModel()
        {
            return new ExaminationReport()
            {
                Content = Content,
                DoctorId = DoctorId,
                Prescriptions = Prescriptions,
                Symptoms = Symptoms,
                Examination = Examination,
            };
        }
    }
}