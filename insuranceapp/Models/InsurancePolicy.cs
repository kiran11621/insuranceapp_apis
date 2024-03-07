namespace insuranceapp.Models
{
    public enum InsuranceType
    {
        Life,
        Health,
        Auto,
        Home
    }

    public class PolicyHolder
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class InsurancePolicy
    {
        public int PolicyNumber { get; set; }
        public PolicyHolder PolicyHolder { get; set; }
        public InsuranceType Type { get; set; }
        public double Premium { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
