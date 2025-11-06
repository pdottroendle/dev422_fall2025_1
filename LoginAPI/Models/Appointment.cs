using System;

namespace HealthcareAppointmentsAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
		public string? Username { get; set; }
		public string PatientUsername { get; set; } = string.Empty;
		public string DoctorUsername { get; set; } = string.Empty;
		public string Reason { get; set; } = string.Empty;
		public DateTime AppointmentDate { get; set; }
		public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    }
}