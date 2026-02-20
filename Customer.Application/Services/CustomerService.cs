using Customer.Application.DTOs;
using Customer.Application.Interfaces;
using Customer.Domain.Entities;
using Customer.Domain.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Customer.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CustomerService(ICustomerRepository customerRepository, IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<BaseResponse<bool>> Register(CreateCustomerRequest request)
        {
            var exist = await _customerRepository.CheckIfEmailExistAsync(request.Email);
            if (exist)
            {
                return BaseResponse<bool>.Failure("Email already exist");
            }

            string hashedPassword = await _passwordHasher.HashAsync(request.Password);

            try
            {
                var newCustomer = new CustomerDomain(
                        request.FirstName,
                        request.LastName,
                        request.Age,
                        request.Gender,
                        request.Email,
                        hashedPassword
                    );

                await _customerRepository.AddCustomerAsync(newCustomer);

                return BaseResponse<bool>.Success(true, "Customer Registered Successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.Failure(ex.Message);
            }
        }
        public async Task<BaseResponse<bool>> Login(LoginRequest request)
        {
            var user = await _customerRepository.GetCustomerAsync(request.Email);
            if (user == null)
                return BaseResponse<bool>.Failure("User not found.");

            else
            {
                bool verifyPassword = await _passwordHasher.CheckPasswordAsync(request.Password, user.Password);

                if (verifyPassword)
                    return BaseResponse<bool>.Success(true, "Customer Logged In Successfully");
                else
                    return BaseResponse<bool>.Failure("Wrong Password or Email");

            }
        }

        public async Task<BaseResponse<bool>> Update(UpdateCustomerRequest request)
        {

            var existingCustomer = await _customerRepository.GetByIdAsync(request.Id);

            if (existingCustomer == null)
                return BaseResponse<bool>.Failure("Customer Not Found");


            if (existingCustomer.Email != request.Email.Trim().ToLower())
            {
                if (await _customerRepository.CheckIfEmailExistAsync(request.Email))
                    return BaseResponse<bool>.Failure("Email already in use");
            }

 
            string finalPassword = existingCustomer.Password; 
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                finalPassword = await _passwordHasher.HashAsync(request.Password);
            }

            try
            {

                existingCustomer.UpdateDetails(
                    request.FirstName,
                    request.LastName,
                    request.Age,
                    request.Gender,
                    request.Email,
                    finalPassword
                );

                await _customerRepository.UpdateCustomerAsync(existingCustomer);

                return BaseResponse<bool>.Success(true, "Customer Informations Updated Successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.Failure(ex.Message);
            }
        }


        public async Task<BaseResponse<bool>> Delete(uint id)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);

            if (existingCustomer == null)
                return BaseResponse<bool>.Failure("Customer Not Found");

            try
            {
                await _customerRepository.DeleteCustomerAsync(id);

                return BaseResponse<bool>.Success(true, "Customer Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.Failure("An error occured while deleting customer: " + ex.Message);
            }
        }
    }
}
