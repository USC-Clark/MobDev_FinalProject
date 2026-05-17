using System.Linq;
using GymWorkoutApi.Data;
using GymWorkoutApi.DTOs;
using GymWorkoutApi.Models;
using GymWorkoutApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymWorkoutApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            // Check if email already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email already registered" });
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = PasswordService.Hash(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || !PasswordService.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(new
            {
                message = "Login successful",
                user.Id,
                user.Username,
                user.Email
            });
        }
    }
}
