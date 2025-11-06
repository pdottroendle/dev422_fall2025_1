using Xunit;
using HealthcareAppointmentsAPI.Controllers;
using HealthcareAppointmentsAPI.Models;
using HealthcareAppointmentsAPI.Services.Interfaces;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HealthcareAppointmentsAPI.Tests
{
    public class AppointmentControllerTests
    {
        [Fact]
        public void GetAppointmentsByUsername_ReturnsAppointmentsForUser()
        {
            // Arrange
            var mockService = new Mock<IAppointmentService>();
            mockService.Setup(s => s.GetAppointmentsByUser("patient1"))
                       .Returns(new List<Appointment>
                       {
                           new Appointment
                           {
                               Id = 1,
                               PatientUsername = "patient1",
                               DoctorUsername = "doctor1",
                               Status = AppointmentStatus.Confirmed
                           }
                       });

            var controller = new AppointmentController(mockService.Object);

            // Act
            var result = controller.GetAppointmentsByUsername("patient1") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var appointments = Assert.IsType<List<Appointment>>(result.Value);
            Assert.Single(appointments);
            Assert.Equal("patient1", appointments[0].PatientUsername);
        }

        [Fact]
        public void GetAllAppointments_ReturnsAllAppointments()
        {
            // Arrange
            var mockService = new Mock<IAppointmentService>();
            mockService.Setup(s => s.GetAllAppointments())
                       .Returns(new List<Appointment>
                       {
                           new Appointment { Id = 1, PatientUsername = "patient1" },
                           new Appointment { Id = 2, PatientUsername = "patient2" }
                       });

            var controller = new AppointmentController(mockService.Object);

            // Act
            var result = controller.GetAllAppointments() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var appointments = Assert.IsType<List<Appointment>>(result.Value);
            Assert.Equal(2, appointments.Count);
        }

        [Fact]
        public void UpdateStatus_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var mockService = new Mock<IAppointmentService>();
            mockService.Setup(s => s.UpdateStatus(1, AppointmentStatus.Confirmed)).Returns(true);

            var controller = new AppointmentController(mockService.Object);

            // Act
            var result = controller.UpdateStatus(1, AppointmentStatus.Confirmed) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Status updated.", ((dynamic)result.Value).Message);
        }

        [Fact]
        public void DeleteAppointment_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var mockService = new Mock<IAppointmentService>();
            mockService.Setup(s => s.Delete(1)).Returns(true);

            var controller = new AppointmentController(mockService.Object);

            // Act
            var result = controller.DeleteAppointment(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Appointment deleted.", ((dynamic)result.Value).Message);
        }
    }
}