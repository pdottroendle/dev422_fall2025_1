using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using HealthcareAppointmentsAPI.Controllers;
using HealthcareAppointmentsAPI.Models;
using HealthcareAppointmentsAPI.Services;
using HealthcareAppointmentsAPI.Interfaces;
using System.Collections.Generic;

public class AuthControllerTests
{
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:SecretKey", "supersecretkey1234567890"}
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        IUserService userService = new UserService();
        _controller = new AuthController(userService, configuration);
    }

    [Fact]
    public void Register_WithValidUser_ReturnsOk()
    {
        var result = _controller.Register(new User {
            Username = "testuser",
            Password = "password123",
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            Role = "Patient"
        });

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Login_WithInvalidUser_ReturnsUnauthorized()
    {
        var result = _controller.Login(new UserLogin {
            Username = "nonexistent",
            Password = "wrongpass"
        });

        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}