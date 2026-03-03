using Customer.Application.Interfaces;
using Customer.Domain.Entities;
using Customer.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Customer.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerInfo?> GetByIdAsync(uint id)
        {
            return await _context.Customers
                .Include(x => x.LoginInfo)   
                .Include(x => x.BalanceInfo)
                .FirstOrDefaultAsync(x => x.Id == id);
            // virtual and eager loading nedir ??
            // as no tracking
            // dbset iqueryable. ienumarable olsa direk select * atar icerde filtrelerdi
        }


        public async Task AddAsync(CustomerInfo customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        public async Task AddAsyncParallel(CustomerInfo customer)
        {
            var databaseTask = Task.Run(async () =>
            {
                Debug.WriteLine("DB İşlemi Başladı...");
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                Debug.WriteLine("DB İşlemi Bitti.");
            });

            var dummyLoggerTask = Task.Run(async () =>
            {
                Debug.WriteLine("dummyLoggerTask Başladı");
                await Task.Delay(1000);
                Debug.WriteLine("dummyLoggerTask Bitti.");
            });

            await Task.WhenAll(databaseTask, dummyLoggerTask);

            Console.WriteLine("Tüm görevler tamamlandı, kullanıcıya dönülüyor.");
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.CustomerLogins.AnyAsync(x => x.Email == email);
        }

        public async Task UpdateAsync(CustomerInfo customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(uint id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CustomerInfo?> GetByEmailAsync(string email)
        {
            return await _context.Customers
                .Include(x => x.LoginInfo)
                .Include(x => x.BalanceInfo)
                .FirstOrDefaultAsync(x => x.LoginInfo.Email == email.Trim().ToLower());
        }

        public async Task<IEnumerable<CustomerInfo>> GetAllAsync()
        {
            return await _context.Customers
                .Include(x => x.LoginInfo)
                .Include(x => x.BalanceInfo)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IList<CustomerInfo>?> GetAllAsync1() => await (await GetAllAsync() as Task<List<CustomerInfo>>)!;

        public async Task<CustomerInfo[]?> GetAll2Async() => (await GetAllAsync()).ToArray();

        public async Task<List<CustomerInfo>?> GetAllAsync3() => (await GetAllAsync()).ToList();
    }

}
