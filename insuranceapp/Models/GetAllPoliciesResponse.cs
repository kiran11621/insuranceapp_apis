namespace insuranceapp.Models
{
    public class GetAllPoliciesResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<InsurancePolicy> Policies { get; set; }
    }

}
