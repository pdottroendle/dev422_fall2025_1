using Xunit;
using Microsoft.Extensions.Configuration;
using HealthcareAppointmentsAPI.Controllers;
using HealthcareAppointmentsAPI.Models;
using System.Collections.Generic;

namespace HealthcareAppointmentsAPI.Tests
{
    public class AuthControllerTests
    {
        [Fact]
        public void Login_WithValidCredentials_ReturnsToken()
        {
            // Arrange
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

            var configData = new List<KeyValuePair<string, string?>>
            {
                new("Jwt:SecretKey", "SOEeJbyh5TAogrwLKTM3Ku1bxG+KXUwHMmZ/HjiVya8=")
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();

            var controller = new AuthController(users, configuration);

            var loginDto = new UserLogin
            {
                Username = "admin1",
                Password = "StrongPass456!"
            };

            // Act
            var result = controller.Login(loginDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Register_NewUser_ReturnsSuccess()
        {
            var users = new List<User>();

            var configData = new List<KeyValuePair<string, string?>>
            {
                new("Jwt:SecretKey", "SOEeJbyh5TAogrwLKTM3Ku1bxG+KXUwHMmZ/HjiVya8=")
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();

            var controller = new AuthController(users, configuration);

            var newUser = new UserRegisterDto
            {
                Username = "newuser",
                Password = "NewPass123!",
                Email = "newuser@example.com",
                FirstName = "New",
                LastName = "User",
                Role = "User"
            };

            // Act
            var result = controller.Register(newUser);

            // Assert
            Assert.NotNull(result);
        }
    }
}
