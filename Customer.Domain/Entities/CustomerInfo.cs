using Customer.Domain.Enums;

namespace Customer.Domain.Entities
{
    public class CustomerInfo
    {
        public uint Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public byte Age { get; private set; }
        public Gender Gender { get; private set; }

        public CustomerLogin LoginInfo { get; private set; } = null!;
        public CustomerBalance BalanceInfo { get; private set; } = null!;

        private CustomerInfo() { }

        public CustomerInfo(string firstName, string lastName, byte age, Gender gender)
        {
            ValidateNames(firstName, lastName);

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Age = age;
            Gender = gender;
        }

        public void UpdateDetails(string firstName, string lastName, byte age, Gender gender)
        {
            ValidateNames(firstName, lastName);

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Age = age;
            Gender = gender;
        }

        private void ValidateNames(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new Exception("First name cannot be empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new Exception("Last name cannot be empty.");
        }

        public void SetLoginInfo(CustomerLogin login) => LoginInfo = login;
        public void SetBalanceInfo(CustomerBalance balance) => BalanceInfo = balance;
    }
}