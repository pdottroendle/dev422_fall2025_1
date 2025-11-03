using HealthcareAppointmentsAPI.Interfaces;
using HealthcareAppointmentsAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace HealthcareAppointmentsAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly List<Appointment> _appointments = new();
        private int _nextId = 1;

        public Appointment Create(Appointment appointment)
        {
            appointment.Id = _nextId++;
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

        public bool UpdateStatus(int id, AppointmentStatus status)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                appointment.Status = status;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                _appointments.Remove(appointment);
                return true;
            }
            return false;
        }
    }
}