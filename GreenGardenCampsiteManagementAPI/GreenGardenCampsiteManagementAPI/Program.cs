using AutoMapper;
using BusinessObject.Models;
using GreenGardenCampsiteManagementAPI.ConfigAutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Accounts;
using Repositories.Activities;
using Repositories.IReponsitory;
using Repositories.Orders;
using Repositories.Reponsitory;
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
//builder.Services.AddScoped<IActivityReponsitory, ActivityReponsitory>();
//builder.Services.AddScoped<ICampingCategoryReponsitory, CampingCategoryReponsitory>();
//builder.Services.AddScoped<ICampingGearReponsitory, CampingGearReponsitory>();
//builder.Services.AddScoped<IComboCampingGearDetaiReponsitory, ComboCampingGearDetaiReponsitory>();
//builder.Services.AddScoped<IComboFootDetailReponsitory, ComboFootDetailReponsitory>();
//builder.Services.AddScoped<IComboReponsitory, ComboReponsitory>();
//builder.Services.AddScoped<IComboTicketDetailReponsitory, ComboTicketDetailReponsitory>();
//builder.Services.AddScoped<IEventReponsitory, EventReponsitory>();
//builder.Services.AddScoped<IFoodAndDrinkCategoryReponsitory, FoodAndDrinkCategoryReponsitory>();
//builder.Services.AddScoped<IFoodAndDrinkReponsitory, FoodAndDrinkReponsitory>();
//builder.Services.AddScoped<IFoodComboItemReponsitory, FoodComboItemReponsitory>();
//builder.Services.AddScoped<IFoodComboReponsitory, FoodComboReponsitory>();
//builder.Services.AddScoped<IOrderCampingGearDetailReponsitory, OrderCampingGearDetailReponsitory>();
//builder.Services.AddScoped<IOrderComboDetailReponsitory, OrderComboDetailReponsitory>();
//builder.Services.AddScoped<IOrderFoodComboDetailReponsitory, OrderFoodComboDetailReponsitory>();
//builder.Services.AddScoped<IOrderFoodDetailReponsitory, OrderFoodDetailReponsitory>();
//builder.Services.AddScoped<IOrderReponsitory, OrderReponsitory>();
//builder.Services.AddScoped<IOrderTicketDetailReponsitory, OrderTicketDetailReponsitory>();
//builder.Services.AddScoped<ITicketCategoryReponsitory, TicketCategoryReponsitory>();
//builder.Services.AddScoped<ITicketReponsitory, TicketReponsitory>();
builder.Services.AddScoped<IUserReponsitory, UserReponsitory>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IActivityRepository, ActiviyRepository>();

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
