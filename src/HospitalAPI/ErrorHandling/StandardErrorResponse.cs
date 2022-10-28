using Newtonsoft.Json;

namespace HospitalAPI.ErrorHandling
{
    public class StandardErrorResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}