using Customer.Domain.Enums;

namespace Customer.Application.DTOs
{
    public class CreateCustomerRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get;  set; } = string.Empty;
        public byte Age { get;  set; }
        public Gender Gender { get; set; }
        public string Email { get;  set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}
