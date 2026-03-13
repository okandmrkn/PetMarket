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

        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private static int _sharedValue = 0;

        public async Task AddAsyncParallel(CustomerInfo customer)
        {
            var databaseTask = Task.Run(async () =>
            {
                await _semaphore.WaitAsync();
                try
                {
                    Interlocked.Add(ref _sharedValue, 5);
                    Debug.WriteLine($"DB: Değer +5 artırıldı. Yeni Değer: {_sharedValue}");

                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                }
                finally { _semaphore.Release(); }
            });

            var dummyLoggerTask = Task.Run(async () =>
            {
                await _semaphore.WaitAsync();
                
                try
                {
                    
                    int initial, computed;
                    do
                    {
                        initial = _sharedValue;
                        computed = initial * 2;
                    } while (initial != Interlocked.CompareExchange(ref _sharedValue, computed, initial));

                    Debug.WriteLine($"Logger: Değer *2 yapıldı. Yeni Değer: {_sharedValue}");
                    await Task.Delay(1000);
                }
                finally { _semaphore.Release(); }
            });

            await Task.WhenAll(databaseTask, dummyLoggerTask);
            Debug.WriteLine($"Final Paylaşılan Değer: {_sharedValue}");
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
