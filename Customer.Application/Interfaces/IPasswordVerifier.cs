using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Application.Interfaces
{
    public interface IPasswordVerifier
    {
        Task<bool> CheckPasswordAsync(string password, string hashedPassword);
    }
}
