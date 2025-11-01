using HealthcareAppointmentsAPI.Models;

namespace HealthcareAppointmentsAPI.Interfaces
{
    public interface IAppointmentService
    {
        Appointment Create(Appointment appointment);
        List<Appointment> GetAppointmentsByUser(string username);
        List<Appointment> GetAllAppointments();
        bool UpdateStatus(int id, string status);
    }
}