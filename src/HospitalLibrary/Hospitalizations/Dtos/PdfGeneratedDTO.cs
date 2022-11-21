namespace HospitalLibrary.Hospitalizations.Dtos
{
    public class PdfGeneratedDTO
    {
        public string Url { get; set; }

        public PdfGeneratedDTO(string url)
        {
            Url = url;
        }
    }
}