using Customer.Application.Interfaces;
using Customer.Application.Services;
using Customer.Infrastructure.Security;
using Customer.Persistence.Context;
using Customer.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVİS KAYITLARI (DI Container) ---

builder.Services.AddControllers();

#region SWAGGER AYARLARI (Eksik Olan Kısım 1)
// API uç noktalarını keşfetmek ve dökümante etmek için gerekli
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

// Veritabanı Bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var cryptService = new BCryptPasswordService();
// Repository ve Servis Kayıtları
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPasswordHasher>((service) => cryptService);
builder.Services.AddScoped<IPasswordVerifier>((service) => cryptService);
builder.Services.AddScoped<CustomerService>();

var app = builder.Build();



// --- 2. MIDDLEWARE AYARLARI (İstek Boru Hattı) ---

#region SWAGGER MIDDLEWARE (Eksik Olan Kısım 2)
// Sadece geliştirme ortamında Swagger arayüzünü açar
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Bu satır localhost/swagger sayfasını oluşturan satırdır!
}
#endregion

app.UseHttpsRedirection();

// Güvenlik katmanları (Sıralama önemlidir)
app.UseAuthorization();

// İstekleri Controller'lara yönlendirir
app.MapControllers();

app.Run();