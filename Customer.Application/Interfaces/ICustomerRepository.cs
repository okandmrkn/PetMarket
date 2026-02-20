using Customer.Domain.Entities;

namespace Customer.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(CustomerDomain customer);
        Task<CustomerDomain?> GetCustomerAsync(string email);
        Task<bool> CheckIfEmailExistAsync(string email);
        Task UpdateCustomerAsync(CustomerDomain customer);
        Task DeleteCustomerAsync(uint id);
        Task<CustomerDomain?> GetByIdAsync(uint id);
        Task<IEnumerable<CustomerDomain>> GetAllAsync();
        Task<IList<CustomerDomain>?> GetAllAsync1();
        Task<CustomerDomain[]?> GetAll2Async();
        Task<List<CustomerDomain>?> GetAllAsync3();
        

    }
}
