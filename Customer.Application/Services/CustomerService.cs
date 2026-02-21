using Customer.Application.DTOs;
using Customer.Application.Interfaces;
using Customer.Domain.Entities;

namespace Customer.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordVerifier _passwordVerifier;

        public CustomerService(ICustomerRepository customerRepository, IPasswordHasher passwordHasher, IPasswordVerifier passwordVerifier)
        {
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
            _passwordVerifier = passwordVerifier;
        }

        public async Task<BaseResponse<bool>> Update(UpdateCustomerRequest request)
        {
            var existing = await _customerRepository.GetByIdAsync(request.Id);
            if (existing == null) return BaseResponse<bool>.Failure("User Not Found");

            
            if (existing.LoginInfo.Email != request.Email.Trim().ToLower())
            {
                if (await _customerRepository.ExistsByEmailAsync(request.Email))
                    return BaseResponse<bool>.Failure("Email already in use");
            }

            try
            {
                string finalPassword = existing.LoginInfo.Password;
                if (!string.IsNullOrWhiteSpace(request.Password))
                {
                    if (request.Password.Length < 8) return BaseResponse<bool>.Failure("Password Too short");
                    finalPassword = await _passwordHasher.HashAsync(request.Password);
                }

                existing.UpdateDetails(request.FirstName, request.LastName, request.Age, request.Gender);
                existing.LoginInfo.UpdateLogin(request.Email, finalPassword);

                await _customerRepository.UpdateAsync(existing);
                return BaseResponse<bool>.Success(true, "Updated");
            }
            catch (Exception ex) { return BaseResponse<bool>.Failure(ex.Message); }
        }

        public async Task<BaseResponse<bool>> Delete(uint id)
        {
            if (await _customerRepository.GetByIdAsync(id) == null)
                return BaseResponse<bool>.Failure("User Not Found");

            await _customerRepository.DeleteAsync(id);
            return BaseResponse<bool>.Success(true, "User Deleted");
        }

        public async Task<BaseResponse<bool>> Register(CreateCustomerRequest request)
        {
            if (request.Password.Length < 8)
                return BaseResponse<bool>.Failure("Password must be at least 8 characters.");

            if (await _customerRepository.ExistsByEmailAsync(request.Email))
                return BaseResponse<bool>.Failure("Email already exists.");

            try
            {
                string hashedPassword = await _passwordHasher.HashAsync(request.Password);

                var login = new CustomerLogin(request.Email, hashedPassword);
                var balance = new CustomerBalance(0);
                var customer = new CustomerInfo(request.FirstName, request.LastName, request.Age, request.Gender);

                customer.SetLoginInfo(login);
                customer.SetBalanceInfo(balance);

                await _customerRepository.AddAsync(customer);
                return BaseResponse<bool>.Success(true, "Registered Successfully");
            }
            catch (Exception ex) { return BaseResponse<bool>.Failure(ex.Message); }
        }

        public async Task<BaseResponse<bool>> Login(LoginRequest request)
        {
            var user = await _customerRepository.GetByEmailAsync(request.Email);
            if (user == null) return BaseResponse<bool>.Failure("User not found.");

            bool verify = await _passwordVerifier.CheckPasswordAsync(request.Password, user.LoginInfo.Password);
            return verify ? BaseResponse<bool>.Success(true, "Logged In") : BaseResponse<bool>.Failure("Wrong Credentials");
        }

        public async Task<BaseResponse<CustomerResponse>> GetById(uint id)
        {

            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return BaseResponse<CustomerResponse>.Failure("Customer not found.");

            var response = MapToResponse(customer);

            return BaseResponse<CustomerResponse>.Success(response);
        }

        public async Task<BaseResponse<List<CustomerResponse>>> GetAll()
        {
            var customers = await _customerRepository.GetAllAsync();

            var responseList = customers.Select(c => MapToResponse(c)).ToList();

            return BaseResponse<List<CustomerResponse>>.Success(responseList);
        }

        private CustomerResponse MapToResponse(CustomerInfo customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                Gender = customer.Gender.ToString(),
                Email = customer.LoginInfo.Email,
                Balance = customer.BalanceInfo.Balance
            };
        }
    }
}