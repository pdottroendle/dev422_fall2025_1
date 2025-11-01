using HealthcareAppointmentsAPI.Models;
using HealthcareAppointmentsAPI.Interfaces;

namespace HealthcareAppointmentsAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly List<Appointment> _appointments = new();
        private int _nextId = 1;

        public Appointment Create(Appointment appointment)
        {
            appointment.Id = _nextId++;
            appointment.Status = AppointmentStatus.Pending;
            _appointments.Add(appointment);
            return appointment;
        }

        public List<Appointment> GetAppointmentsByUser(string username)
        {
            return _appointments.Where(a => a.PatientUsername == username || a.DoctorUsername == username).ToList();
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointments;
        }

        public bool UpdateStatus(int id, string status)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null) return false;
            if (Enum.TryParse(status, out AppointmentStatus newStatus))
            {
                appointment.Status = newStatus;
                return true;
            }
            return false;
        }
    }
}