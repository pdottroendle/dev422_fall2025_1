using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HealthcareAppointmentsAPI.Controllers;
using HealthcareAppointmentsAPI.Models;
using Microsoft.Extensions.Configuration;

namespace HealthcareAppointmentsAPI.Tests
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly List<User> _users;
        private readonly IConfiguration _config;

        public AuthControllerTests()
        {
            _users = new List<User>();
            _config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
            {
                { "Jwt:Key", "test-secret-key" }
            }).Build();

            _controller = new AuthController(_users, _config);
        }

        [Fact]
        public void Register_AddsUser_ReturnsOk()
        {
            var dto = new UserRegisterDto
            {
                Username = "testuser",
                Password = "password",
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                Role = "Patient"
            };

            var result = _controller.Register(dto);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            var dto = new LoginRequest { Username = "wrong", Password = "wrong" };
            var result = _controller.Login(dto);
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}