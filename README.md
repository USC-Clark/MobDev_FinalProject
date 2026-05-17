# ApexFit - Gym Workout App
### Developed by: Jay Nino Clark B. Basilgo
### Course: BSIT - Mobile Development (Final Project)

# Testing Guide

---

## About
ApexFit is a gym workout mobile application that allows users to register, log workouts, and track body weight progress. Built with .NET MAUI (Android) and ASP.NET Core Web API (.NET 9), connected to a MySQL database via Entity Framework Core.

---

## Features
- User Registration and Login (BCrypt password hashing)
- Home Dashboard with live stats
- Workout logging with full CRUD
- Body weight Progress Tracker
- Profile screen
- Black and yellow modern UI theme

---

## Configuring the IP Address
Before running the frontend, update the API base URL to match your machine's IPv4 address.

Open `GymWorkoutAppUI → Services → ApiConfig.cs` and change:
```csharp
public const string BaseUrl = "http://YOUR_IP_HERE:5275";
```

> The API runs on port **5275** by default. Confirm this in `Gym_Workout_API → Properties → launchSettings.json` if needed.

---

## Database
Import the included `apexfit_db.sql` to restore the schema and any existing data.
