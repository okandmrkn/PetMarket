using Customer.Domain.Entities;

namespace Customer.Application.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(CustomerDomain customer);

        CustomerDomain? GetCustomer(string email);

        bool CheckIfEmailExist(string email);

        void UpdateCustomer(CustomerDomain customer); 

        void DeleteCustomer(uint id);

        CustomerDomain? GetById(uint id);

        CustomerDomain[]? GetAll(uint id);
        List<CustomerDomain>? GetAll1(uint id);
        IEnumerable<CustomerDomain>? GetAll2(uint id);
        IList<CustomerDomain>? GetAll3(uint id);

    }
}
