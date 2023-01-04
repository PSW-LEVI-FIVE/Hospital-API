namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos
{
    public class SpecialtyCountDTO
    {
        public string Specialty { get; set; }
        public int Successful { get; set; }
        public int Unsuccessful { get; set; }

        public SpecialtyCountDTO(string specialty, int successful, int unsuccessful)
        {
            Specialty = specialty;
            Successful = successful;
            Unsuccessful = unsuccessful;
        }
    }
}