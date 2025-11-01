using HealthcareAppointmentsAPI.Models;

namespace HealthcareAppointmentsAPI.Interfaces
{
    public interface IUserService
    {
        bool Register(User user);
        string Login(string username, string password);
        User GetUser(string username);
        bool VerifyPassword(string password, string hashedPassword);
    }
}