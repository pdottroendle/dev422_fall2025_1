using System;

namespace HealthcareAppointmentsAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientUsername { get; set; }
        public string DoctorUsername { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}