namespace Customer.Domain.Entities
{
    public class CustomerBalance
    {
        public uint Id { get; private set; }
        public decimal Balance { get; private set; }

        public CustomerInfo Customer { get; private set; } = null!;

        private CustomerBalance() { }

        public CustomerBalance(decimal initialBalance = 0)
        {
            if (initialBalance < 0) throw new Exception("Initial balance cannot be negative.");
            Balance = initialBalance;
        }

        public void AddBalance(decimal amount)
        {
            if (amount <= 0) throw new Exception("Amount must be bigger than 0.");
            Balance += amount;
        }

        public void WithdrawBalance(decimal amount)
        {
            if (amount > Balance) throw new Exception("Insufficient balance!");
            Balance -= amount;
        }
    }
}