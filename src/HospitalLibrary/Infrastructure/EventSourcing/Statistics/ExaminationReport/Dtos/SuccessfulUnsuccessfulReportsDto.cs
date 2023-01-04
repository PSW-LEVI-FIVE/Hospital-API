namespace HospitalLibrary.Infrastructure.EventSourcing.Statistics.ExaminationReport.Dtos
{
    public class SuccessfulUnsuccessfulReportsDto
    {
        public int Successful { get; set; }
        public int Unsuccessful { get; set; }


        public SuccessfulUnsuccessfulReportsDto(int successful, int unsuccessful)
        {
            Successful =  successful;
            Unsuccessful = unsuccessful;
        }
    }
}