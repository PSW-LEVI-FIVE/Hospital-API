namespace HospitalLibrary.Examination.Dtos
{
    public class ExaminationPdfDto
    {
        public int ExaminationId { get; set; }
        public string Url { get; private set; }

        public ExaminationPdfDto()
        {
            
        }
        public ExaminationPdfDto(int examinationId,string url)
        {
            ExaminationId = examinationId;
            Url = url;
        }
    }
}