using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HealthcareAppointmentsAPI.Controllers;
using HealthcareAppointmentsAPI.Interfaces;
using HealthcareAppointmentsAPI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class AppointmentControllerTests
{
    private readonly AppointmentController _controller;
    private readonly Mock<IAppointmentService> _mockService;

    public AppointmentControllerTests()
    {
        _mockService = new Mock<IAppointmentService>();
        _controller = new AppointmentController(_mockService.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "testuser"),
            new Claim(ClaimTypes.Role, "Patient")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public void BookAppointment_ReturnsOk()
    {
        var appointment = new Appointment
        {
            DoctorUsername = "doctor1",
            AppointmentDate = System.DateTime.Now.AddDays(1),
            Reason = "Checkup"
        };

        _mockService.Setup(s => s.Create(It.IsAny<Appointment>())).Returns(appointment);

        var result = _controller.BookAppointment(appointment);

        Assert.IsType<OkObjectResult>(result);
    }
}