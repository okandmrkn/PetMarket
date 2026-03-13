using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Application.Services;
using Product.Domain.Services;
using Product.Persistence.Context;
using Product.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVİS KAYITLARI (DI Container) ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPriceHistoryManager, PriceHistoryManager>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// --- 2. MIDDLEWARE AYARLARI ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();