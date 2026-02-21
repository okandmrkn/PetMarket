namespace Customer.Application.Interfaces
{
    public interface IPasswordHasher
    {
        Task<string> HashAsync(string password);

    }
}
