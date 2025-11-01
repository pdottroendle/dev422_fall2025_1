Healthcare Appointments API - README

Scaffolding:
-----------

Controllers

>>AuthController.cs
>>AppointmentController.cs


Models

>>User.cs
>>Appointment.cs
>>AppointmentStatus.cs


Services

>>UserService.cs
>>AppointmentService.cs


Tests

>>AuthControllerTests.cs
>>AppointmentControllerTests.cs


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
