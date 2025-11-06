using HealthcareAppointmentsAPI.Models;
using System.Collections.Generic;

namespace HealthcareAppointmentsAPI.Services.Interfaces
{
    public interface IAppointmentService
    {
Appointment Create(Appointment appointment);
List<Appointment> GetAppointmentsByUser(string username);
List<Appointment> GetAllAppointments();
bool UpdateStatus(int id, AppointmentStatus status);
bool Delete(int id);
    }
}