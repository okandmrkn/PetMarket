using Customer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Domain.Entities
{
    public class CustomerDomain
    {
        public uint Id { get; private set; }
        public byte Age { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public Gender Gender { get; private set; }
        public decimal Balance { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;

        private CustomerDomain() { } /* EF Core veritabanindan veri cekerken nesne olusturabilsin diye var */

        public CustomerDomain(string firstName, string lastName, byte age, Gender gender, string email, string passwordHashed)
        {
            Validate(firstName, lastName, email, passwordHashed);

            FirstName = firstName.Trim(); // " Ali " -> "Ali" olur.
            LastName = lastName.Trim(); // bu tarz kontroller burada mı yapılmalı yoksa veri geldikten sonra persistence layerda mı ?
            Age = age;
            Gender = gender;
            Balance = 0;
            Email = email.Trim().ToLower();
            Password = passwordHashed;
        }
        private void Validate(string firstName, string lastName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new Exception("First name cannot be empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new Exception("Last name cannot be empty.");

            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email cannot be empty.");

            if (password.Length < 8)
                throw new Exception("Password too short!");

        }
        public void AddBalance(decimal amount)
        {
            if (amount <= 0)
                throw new Exception("Amount must be bigger than 0.");

            Balance += amount;
        }

        public void WithdrawBalance(decimal amount)
        {
            if (amount > Balance)
                throw new Exception("Insufficient balance!");

            Balance -= amount;
        }

        public void UpdateDetails(string firstName, string lastName, byte age, Gender gender, string email, string password)
        {

            Validate(firstName, lastName, email, password);

            this.FirstName = firstName.Trim();
            this.LastName = lastName.Trim();
            this.Age = age;
            this.Gender = gender;
            this.Email = email.Trim().ToLower();
            this.Password = password;
        }
    }
}


