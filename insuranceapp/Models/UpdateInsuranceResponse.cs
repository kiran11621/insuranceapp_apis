namespace insuranceapp.Models
{
    public class UpdateInsuranceResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public InsurancePolicy UpdatedPolicy { get; set; }
    }

}
