Healthcare Appointments API - README
Notes Student TROENDLE DEV422 PROF USMAN BELLEVUE COLLEGE FALL 2025

Crucial details
---------------

EF added>>>>>>>>>>>>>>>>>>>>
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory

added Model
using Microsoft.EntityFrameworkCore;

namespace HealthcareAppointmentsAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; } // If you have a User model
    }
}

using Microsoft.EntityFrameworkCore;

program.cs
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("HealthcareDB")); // For testing, or UseSqlServer for real DB

>>>>>>>>>>>>>>>>PRoper scaffolding Controller - Auto 
dotnet aspnet-codegenerator controller \
    -name AppointmentController \
    -m Appointment \
    -dc ApplicationDbContext \
    --useDefaultLayout \
    --referenceScriptLibraries

>>>>>>>>>>>>>>>JWT

Set Authorization at the Collection Level:
Go to LoginAPI Collection → Authorization.
Choose Bearer Token.
In the Token field, enter {{jwtToken}}.
Save the collection.

>>>>>>>>>>>>>>>>Hash Passwords
Hash the stored Passwords
dotnet add package BCrypt.Net-Next
Jwt key is generated like this per ex
openssl rand -base64 32

Program.cs, define the in-memory admin user:
C#var users = new List<User>{    new User    {        Username = "admin1",        Password = BCrypt.Net.BCrypt.HashPassword("StrongPass456!"),        Email = "admin@example.com",        FirstName = "System",        LastName = "Admin",        Role = "Admin"    }};

This hashes the password securely. Later, when validating login:
C#BCrypt.Net.BCrypt.Verify(inputPassword, storedHashedPassword)

You need to install the BCrypt.Net-Next NuGet package:
Shelldotnet add package BCrypt.Net-NextShow more lines
Then add this using directive in Program.cs and AuthController.cs:
C#using BCrypt.Net;Show more lines

This resolves the BCrypt 

>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>CORS for multiple access
Add CORS Configuration
Add this before var app = builder.Build(); in Program.cs:
C#builder.Services.AddCors(options =>{    options.AddPolicy("AllowAll", policy =>        policy.AllowAnyOrigin()              .AllowAnyHeader()              .AllowAnyMethod());});

Then, after app.UseHttpsRedirection();, add:
C#app.UseCors("AllowAll");

This enables CORS for all origins, headers, and methods


Scaffolding:
-----------

LoginAPI/
│
├── Controllers/
│   ├── AuthController.cs
│   ├── AppointmentController.cs
│
├── Models/
│   ├── User.cs
│   ├── UserLogin.cs
│   ├── UserRegisterDto.cs
│   ├── Appointment.cs
│
├── Services/
│   ├── UserService.cs
│   ├── AppointmentService.cs
│   ├── Interfaces/
│       ├── IUserService.cs
│       ├── IAppointmentService.cs
│
├── Tests/
│   ├── AuthControllerTests.cs
│   ├── AppointmentControllerTests.cs
│
├── Program.cs
├── appsettings.json 

Setup Instructions
------------------

Prerequisites:.NET 8.0 SDK installed
Visual Studio 2022 or later / VS Code
SQL Server or SQLite (depending on your configuration)

Clone the Repository:
git clone https://github.com/your-repo/healthcare-appointments-api.git
   cd healthcare-appointments-api

Configure the Application:Update appsettings.json with your database connection string and JWT secret key.
Run Database Migrations:
dotnet ef database update

Nuget Configuration
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Moq
dotnet add package Microsoft.NET.Test.Sdk


Run the Application:
dotnet run

Access the API:Swagger UI: https://localhost:5001/swagger

Sample Console Sessions
Register a New User
POST /api/auth/register
{
  "username": "johndoe",
  "password": "SecurePass123!",
  "role": "Patient"
}


Login and Get JWT Token
POST /api/auth/login
{
  "username": "johndoe",
  "password": "SecurePass123!"
}

_Response:_
{
  "token": "<JWT_TOKEN>"
}

Book an Appointment (Patient Role)
POST /api/appointments
Authorization: Bearer <JWT_TOKEN>
{
  "doctorId": 2,
  "appointmentDate": "2025-11-10T14:00:00Z",
  "reason": "Routine check-up"
}

Assumptions Made
----------------

Users are assigned roles at registration and cannot change roles afterward.
JWT tokens expire after 30 minutes and can be refreshed using a refresh token endpoint.
Admins can view and manage all appointments.
Doctors can only view and manage their own appointments.
Patients can only view and manage their own appointments.
Appointment statuses include: Pending, Confirmed, and Canceled.
Input validation is enforced using Data Annotations and FluentValidation.
Error handling returns standardized error responses with appropriate HTTP status codes.


Notes
-----

Ensure HTTPS is enabled in production.
Use dependency injection for all services and repositories.
Follow SOLID principles and avoid hard-coded logic for roles or device types.
Extendable architecture allows adding new roles or appointment types with minimal changes.


>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>EF Core-based persistence means using Entity Framework Core (EF Core) as your Object-Relational Mapper (ORM) to store and retrieve data from a real database instead of an in-memory list.

EF Core Does - for production so its not just stored in memory but in databases that can migrate

Maps your C# classes (like Appointment, User) to database tables.
Handles CRUD operations automatically via LINQ.
Supports migrations to evolve your schema over time.
Works with SQL Server, PostgreSQL, MySQL, SQLite, etc.

Current: AppointmentService stores appointments in a List<Appointment> in memory. Data disappears when the app restarts.
EF Core: Stores appointments in a database (e.g., SQL Server). Data persists across restarts.

DbContext Class:
C#public class ApplicationDbContext : DbContext{    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }    public DbSet<Appointment> Appointments { get; set; }    public DbSet<User> Users { get; set; }}Show more lines

Register DbContext in Program.cs:
C#builder.Services.AddDbContext<ApplicationDbContext>(options =>    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));Show more lines

Connection String in appsettings.json:
JSON"ConnectionStrings": {    "DefaultConnection": "Server=.;Database=HealthcareAppointments;Trusted_Connection=True;"}Show more lines

Run Migrations:
Shelldotnet ef migrations add InitialCreatedotnet ef database updateShow more lines

Persistent storage.
Easier queries with LINQ.
Built-in validation and concurrency handling.