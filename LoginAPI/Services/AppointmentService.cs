//using HealthcareAppointmentsAPI.Interfaces;
using HealthcareAppointmentsAPI.Services.Interfaces;
using HealthcareAppointmentsAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace HealthcareAppointmentsAPI.Services
{
  using HealthcareAppointmentsAPI.Models;

  public class AppointmentService : IAppointmentService
  {
    private readonly ApplicationDbContext _context;

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Appointment Create(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        _context.SaveChanges();
        return appointment;
    }

    public List<Appointment> GetAppointmentsByUser(string username)
    {
        return _context.Appointments
            .Where(a => a.PatientUsername == username || a.DoctorUsername == username)
            .ToList();
    }

    public List<Appointment> GetAllAppointments()
    {
        return _context.Appointments.ToList();
    }

    public bool UpdateStatus(int id, AppointmentStatus status)
    {
        var appointment = _context.Appointments.Find(id);
        if (appointment == null) return false;
        appointment.Status = status;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var appointment = _context.Appointments.Find(id);
        if (appointment == null) return false;
        _context.Appointments.Remove(appointment);
        _context.SaveChanges();
        return true;
    }
  }   
}