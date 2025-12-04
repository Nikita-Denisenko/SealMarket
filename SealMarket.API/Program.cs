using Microsoft.EntityFrameworkCore;
using SealMarket.Application.Services;
using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Interfaces.Services;
using SealMarket.Infrastructure.Data;
using SealMarket.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("YourConnectionString"),
    new MySqlServerVersion(new Version(8, 0, 43)))
);

// Repositories DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// Services DI
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();