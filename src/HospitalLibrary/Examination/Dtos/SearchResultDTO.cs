using System;

namespace HospitalLibrary.Examination.Dtos
{
    public class SearchResultDTO
    {
 

        public string DoctorName { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime EndAt { get; set; }
        
        public SearchResultDTO(ExaminationReport examinationReport)
        {
            DoctorName = examinationReport.Doctor.Name + examinationReport.Doctor.Surname;
            Content = examinationReport.Content;
            Url = examinationReport.Url;
            EndAt = examinationReport.Examination.EndAt;
        }
    }
    
    
}