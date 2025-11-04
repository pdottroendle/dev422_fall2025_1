using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HealthcareAppointmentsAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add JWT key to configuration
builder.Configuration["Jwt:Key"] = "your-super-secret-key"; // Replace with a secure key

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Register the user store as a singleton
builder.Services.AddSingleton(users);

// ✅ JWT Authentication setup
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

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
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});


var app = builder.Build();

// ✅ Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication(); // ✅ Fixed typo
app.UseAuthorization();

app.MapControllers();
app.Run();