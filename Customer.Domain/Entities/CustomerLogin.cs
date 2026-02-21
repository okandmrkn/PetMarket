namespace Customer.Domain.Entities
{
    public class CustomerLogin
    {
        public uint Id { get; private set; } 
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;

        public CustomerInfo Customer { get; private set; } = null!;

        private CustomerLogin() { }

        public CustomerLogin(string email, string passwordHashed)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new Exception("Email cannot be empty.");

            Email = email.Trim().ToLower();
            Password = passwordHashed;
        }

        public void UpdateLogin(string email, string hashedPassword)
        {
            Email = email.Trim().ToLower();
            Password = hashedPassword;
        }
    }
}