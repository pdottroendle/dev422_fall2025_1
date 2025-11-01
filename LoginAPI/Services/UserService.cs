using HealthcareAppointmentsAPI.Models;
using HealthcareAppointmentsAPI.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace HealthcareAppointmentsAPI.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new();

        public bool Register(User user)
        {
            if (_users.Any(u => u.Username == user.Username))
                return false;

            user.Password = HashPassword(user.Password);
            _users.Add(user);
            return true;
        }

        public string Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user == null || !VerifyPassword(password, user.Password))
                return null;
            return user.Username;
        }

        public User GetUser(string username) => _users.FirstOrDefault(u => u.Username == username);

        public bool VerifyPassword(string password, string hashedPassword) => HashPassword(password) == hashedPassword;

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}