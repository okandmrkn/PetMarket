using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Application.Interfaces
{
    public interface IPasswordVerifier
    {
        bool CheckPassword(string password, string hashedPassword);
    }
}
