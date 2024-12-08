using AutoMapper;
using BusinessObject.Models;
using GreenGardenCampsiteManagementAPI.ConfigAutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.Accounts;
using Repositories.Activities;
using Repositories.CampingGear;
using Repositories.Combo;
using Repositories.ComboFood;
using Repositories.DashBoard;
using Repositories.Event;
using Repositories.FoodAndDrink;
using Repositories.Orders;
using Repositories.Tickets;
using Repositories.Users;
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
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]); // Đảm bảo bạn lấy Key từ cấu hình
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
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RoleClaimType = "RoleId" // Xác định claim type cho RoleId
    };
});

// Đăng ký các repository vào DI container
builder.Services.AddScoped<IOrderManagementRepository, OrderManagementRepository>();
builder.Services.AddScoped<IActivityRepository, ActiviyRepository>();
builder.Services.AddScoped<IComboRepository, ComboRepository>();
builder.Services.AddScoped<ICampingGearRepository, CampingGearRepository>();
builder.Services.AddScoped<IFoodAndDrinkRepository, FoodAndDrinkRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IComboFoodRepository, ComboFoodRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IDashBoardRepository, DashBoardRepository>();



// Đăng ký AutoMapper
builder.Services.AddSingleton<IMapper>(MapperInstanse.GetMapper());

// Cấu hình các policy cho từng RoleId
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("RoleId", "1"));
    options.AddPolicy("EmployeePolicy", policy => policy.RequireClaim("RoleId", "2"));
    options.AddPolicy("CustomerPolicy", policy => policy.RequireClaim("RoleId", "3"));
    options.AddPolicy("AdminAndEmployeePolicy", policy => policy.RequireRole("1", "2"));
});

// Cấu hình Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Green Garden Campsite Management API", Version = "v1" });

    // Định nghĩa security scheme cho Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Thêm security requirement cho Swagger
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Green Garden Campsite Management API v1"));
}
builder.WebHost.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false); // Set reloadOnChange to false
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
