namespace Customer.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool CheckPassword(string password, string hashedPassword);

    }
}
