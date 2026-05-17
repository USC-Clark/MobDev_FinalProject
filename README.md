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

## System Screenshots
<img width="1080" height="2292" alt="IMG_20260517_143740" src="https://github.com/user-attachments/assets/6fe5553d-5e07-443f-9e1d-83e5261050a7" />

<img width="1080" height="2288" alt="IMG_20260517_143917" src="https://github.com/user-attachments/assets/b3c29ca4-9e6e-4653-af5a-53412b4c6e76" />

<img width="1080" height="2276" alt="IMG_20260517_143757" src="https://github.com/user-attachments/assets/d1e7aaf0-0ebb-43e5-b76a-d7b1817f4dac" />

<img width="1080" height="2288" alt="IMG_20260517_143808" src="https://github.com/user-attachments/assets/8696a45f-49da-48af-966f-bdb41b05a087" />

<img width="1080" height="2288" alt="IMG_20260517_143827" src="https://github.com/user-attachments/assets/05b657a6-8eba-4fa2-980f-82a0b294fd08" />

<img width="1080" height="2288" alt="IMG_20260517_143843" src="https://github.com/user-attachments/assets/ab45becb-c5eb-4d87-95f7-aebf9f83ddbd" />

<img width="1080" height="2288" alt="IMG_20260517_143857" src="https://github.com/user-attachments/assets/8e1d7d54-b663-459d-af98-1f8dfac78b14" />

<img width="1080" height="2288" alt="IMG_20260517_143907" src="https://github.com/user-attachments/assets/e63819e0-9c88-409e-ad92-ee204f5d9b86" />







