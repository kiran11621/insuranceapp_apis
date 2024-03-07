namespace insuranceapp.Models
{
    public class GetPolicyByNumberResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public InsurancePolicy Policy { get; set; }
    }

}
