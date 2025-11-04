using Xunit;
using HealthcareAppointmentsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointmentsAPI.Tests
{
    public class AuthControllerTests
    {
        [Fact]
        public void Login_WithValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var controller = new AuthController();
            var request = new LoginRequest
            {
                Username = "admin",
                Password = "password"
            };

            // Act
            var result = controller.Login(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Login_WithInvalidCredentials_ReturnsUnauthorizedResult()
        {
            // Arrange
            var controller = new AuthController();
            var request = new LoginRequest
            {
                Username = "user",
                Password = "wrong"
            };

            // Act
            var result = controller.Login(request);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}