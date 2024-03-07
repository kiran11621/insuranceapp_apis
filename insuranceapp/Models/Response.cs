namespace insuranceapp.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public int PolicyNumber { get; set; }
        public List<InsurancePolicy> Policies { get; set; }
    }
}
