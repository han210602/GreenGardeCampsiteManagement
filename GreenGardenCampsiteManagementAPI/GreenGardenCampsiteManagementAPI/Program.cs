using AutoMapper;
using BusinessObject.Models;
using GreenGardenCampsiteManagementAPI.ConfigAutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Accounts;
using Repositories.Activities;
using Repositories.CampingGear;
using Repositories.Combo;
using Repositories.FoodAndDrink;
using Repositories.Orders;
using Repositories.Tickets;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Đăng ký DbContext với chuỗi kết nối từ cấu hình
builder.Services.AddDbContext<GreenGardenContext>(options =>
{
    string connectString = builder.Configuration.GetConnectionString("MyCnn");
    options.UseSqlServer(connectString);
});

// Thêm JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        RoleClaimType = ClaimTypes.Role
    };
});

// Đăng ký các repository vào DI container

builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IActivityRepository, ActiviyRepository>();
builder.Services.AddScoped<IComboRepository, ComboRepository>();
builder.Services.AddScoped<ICampingGearRepository, CampingGearRepository>();
builder.Services.AddScoped<IFoodAndDrinkRepository, FoodAndDrinkRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

// Đăng ký AutoMapper
builder.Services.AddSingleton<IMapper>(MapperInstanse.GetMapper());

// Cấu hình Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Thêm Middleware cho Authentication và Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
