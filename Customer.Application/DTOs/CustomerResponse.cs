namespace Customer.Application.DTOs
{
    public class CustomerResponse
    {
        public uint Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public byte Age { get; set; }
        public string Gender { get; set; } = string.Empty;
    }
}