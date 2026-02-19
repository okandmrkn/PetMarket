using Customer.Domain.Entities;
using Customer.Domain.Enums;
using System.Reflection;
using Xunit; 

namespace Customer.Domain.Tests
{
    public class CustomerTests
    {
        [Fact] // Bu bir test fonksiyonudur demek
        public void AddBalance_ValidAmount_ShouldIncreaseBalance()
        {
            // Arrange
            var customer = new Customer.Domain.Entities.CustomerDomain("Ali", "Veli", 25, Gender.Male, "aliveli@gmail.com", "asdf514564");
            decimal amountToAdd = 100;

            // Act
            customer.AddBalance(amountToAdd);

            // Assert
            Assert.Equal(100, customer.Balance);
        }


        [Fact]
        public void WithdrawBalance_InsufficientFunds_ShouldThrowException()
        {
            // Arrange
            var customer = new Customer.Domain.Entities.CustomerDomain("Ayşe", "Fatma", 30, Gender.Female, "aysefatma@gmail.com", "asdf514564");
            customer.AddBalance(50);

            // Act & Assert
            Assert.Throws<Exception>(() => customer.WithdrawBalance(100));
        }

        [Fact]
        public void Constructor_ShouldTrimNames()
        {

            string spacedFirstName = "  Ali  ";
            string spacedLastName = " Veli ";
            string expectedFirstName = "Ali"; 
            string expectedLastName = "Veli";

            var customer = new Customer.Domain.Entities.CustomerDomain(spacedFirstName, spacedLastName, 25, Gender.Male, "aliveli@gmail.com", "asdf514564");

            Assert.Equal(expectedFirstName, customer.FirstName);
            Assert.Equal(expectedLastName, customer.LastName);
        }
    }
}