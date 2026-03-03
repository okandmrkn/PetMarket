namespace Customer.Application.DTOs
{
    public class CustomerResponse
    {
        public uint Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public byte Age { get; set; }
        public string Gender { get; set; } = string.Empty;
    }
}

/*
namespace Customer.Application.DTOs
{
    // Tek bir parantez içinde her şeyi bitiriyoruz. 
    // Bu özellikler varsayılan olarak "init-only" (sadece başlangıçta atanabilir) olur.
    public record CustomerResponse(
        uint Id, 
        string FirstName, 
        string LastName, 
        string Email, 
        decimal Balance, 
        byte Age, 
        string Gender
    );
}
*/

/*
public record CustomerResponse
{
    public uint Id { get; init; } // 'set' yerine 'init' gelmesi kritik!
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public decimal Balance { get; init; }
    public byte Age { get; init; }
    public string Gender { get; init; } = string.Empty;
}

*/