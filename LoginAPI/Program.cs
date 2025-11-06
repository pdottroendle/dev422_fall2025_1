using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HealthcareAppointmentsAPI.Models;
using HealthcareAppointmentsAPI.Services;
using HealthcareAppointmentsAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BCrypt.Net;

var builder = WebApplication.CreateBuilder(args);

// ✅ Get JWT key after builder is declared
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]);

// ✅ In-memory user store with Admin seed
var users = new List<User>
{
    new User
    {
        Username = "admin1",
        Password = BCrypt.Net.BCrypt.HashPassword("StrongPass456!"),
        Email = "admin@example.com",
        FirstName = "System",
        LastName = "Admin",
        Role = "Admin"
    }
};

// ✅ Register services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("HealthcareDB")); // For testing
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(users);
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

// ✅ Configure JWT Authentication with RoleClaimType
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RoleClaimType = ClaimTypes.Role // ✅ Critical for role-based authorization
    };
});

// ✅ Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();