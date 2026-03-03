using Customer.Application.Interfaces;
using BCrypt.Net;

namespace Customer.Infrastructure.Security
{

    public class BCryptPasswordService : IPasswordHasher, IPasswordVerifier
    {

        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool CheckPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}