using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HealthcareAppointmentsAPI.Models;
using HealthcareAppointmentsAPI.Interfaces;

namespace HealthcareAppointmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // Patients book appointments
        [Authorize(Roles = "Patient")]
        [HttpPost("book")]
        public IActionResult BookAppointment([FromBody] Appointment appointment)
        {
            appointment.PatientUsername = User.Identity?.Name;
            var created = _appointmentService.Create(appointment);
            return Ok(created);
        }

        // Patients and Doctors view their own appointments
        [Authorize(Roles = "Patient,Doctor")]
        [HttpGet("my")]
        public IActionResult GetMyAppointments()
        {
            var username = User.Identity?.Name;
            var appointments = _appointmentService.GetAppointmentsByUser(username);
            return Ok(appointments);
        }

        // Admin views all appointments
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IActionResult GetAllAppointments()
        {
            return Ok(_appointmentService.GetAllAppointments());
        }

        // Doctors and Admin update appointment status
        [Authorize(Roles = "Doctor,Admin")]
[HttpPut("status/{id}")]
public IActionResult UpdateStatus(int id, [FromBody] AppointmentStatus status)
{
    if (_appointmentService.UpdateStatus(id, status))
        return Ok(new { Message = "Status updated." });
    return BadRequest("Invalid appointment ID or status.");
}


        // Admin cancels appointment
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            if (_appointmentService.Delete(id))
                return Ok(new { Message = "Appointment deleted." });
            return NotFound("Appointment not found.");
        }
    }
}
