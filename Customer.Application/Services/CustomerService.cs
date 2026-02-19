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

        public BaseResponse<bool> Register(CreateCustomerRequest request)
        {
            var exist = _customerRepository.CheckIfEmailExist(request.Email);
            if (exist)
            {
                return BaseResponse<bool>.Failure("Email already exist");
            }

            string hashedPassword = _passwordHasher.Hash(request.Password);

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

                _customerRepository.AddCustomer(newCustomer);

                return BaseResponse<bool>.Success(true, "Customer Registered Successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.Failure(ex.Message);
            }
        }
        public BaseResponse<bool> Login(LoginRequest request)
        {
            var user = _customerRepository.GetCustomer(request.Email);
            if (user == null)
                return BaseResponse<bool>.Failure("User not found.");

            else
            {
                bool verifyPassword = _passwordHasher.CheckPassword(request.Password, user.Password);

                if (verifyPassword)
                    return BaseResponse<bool>.Success(true, "Customer Logged In Successfully");
                else
                    return BaseResponse<bool>.Failure("Wrong Password or Email");

            }
        }

        public BaseResponse<bool> Update(UpdateCustomerRequest request)
        {

            var existingCustomer = _customerRepository.GetById(request.Id);

            if (existingCustomer == null)
                return BaseResponse<bool>.Failure("Customer Not Found");


            if (existingCustomer.Email != request.Email.Trim().ToLower())
            {
                if (_customerRepository.CheckIfEmailExist(request.Email))
                    return BaseResponse<bool>.Failure("Email already in use");
            }

 
            string finalPassword = existingCustomer.Password; 
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                finalPassword = _passwordHasher.Hash(request.Password);
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

                _customerRepository.UpdateCustomer(existingCustomer);

                return BaseResponse<bool>.Success(true, "Customer Informations Updated Successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.Failure(ex.Message);
            }
        }


        public BaseResponse<bool> Delete(uint id)
        {
            var existingCustomer = _customerRepository.GetById(id);

            if (existingCustomer == null)
                return BaseResponse<bool>.Failure("Customer Not Found");

            try
            {
                _customerRepository.DeleteCustomer(id);

                return BaseResponse<bool>.Success(true, "Customer Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.Failure("An error occured while deleting customer: " + ex.Message);
            }
        }
    }
}
