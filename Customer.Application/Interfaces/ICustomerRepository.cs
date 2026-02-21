using Customer.Domain.Entities;

namespace Customer.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(CustomerInfo customer);
        Task<bool> ExistsByEmailAsync(string email);
        Task UpdateAsync(CustomerInfo customer);
        Task DeleteAsync(uint id);
        Task<CustomerInfo?> GetByIdAsync(uint id);
        Task<CustomerInfo?> GetByEmailAsync(string email);
        Task<IEnumerable<CustomerInfo>> GetAllAsync();
        Task<IList<CustomerInfo>?> GetAllAsync1();
        Task<CustomerInfo[]?> GetAll2Async();
        Task<List<CustomerInfo>?> GetAllAsync3();

        
    }
}
