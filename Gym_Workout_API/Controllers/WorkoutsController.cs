using GymWorkoutApi.Data;
using GymWorkoutApi.DTOs;
using GymWorkoutApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymWorkoutApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkoutsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllWorkouts([FromQuery] int? userId)
        {
            var query = _context.Workouts.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(w => w.UserId == userId.Value);
            }

            var workouts = query
                .OrderByDescending(w => w.WorkoutDate)
                .Select(w => new
                {
                    w.Id,
                    w.UserId,
                    w.WorkoutName,
                    w.WorkoutDate,
                    w.Notes
                })
                .ToList();

            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public IActionResult GetWorkoutById(int id)
        {
            var workout = _context.Workouts
                .Where(w => w.Id == id)
                .Select(w => new
                {
                    w.Id,
                    w.UserId,
                    w.WorkoutName,
                    w.WorkoutDate,
                    w.Notes
                })
                .FirstOrDefault();

            if (workout == null)
            {
                return NotFound(new { message = "Workout not found" });
            }

            return Ok(workout);
        }

        [HttpPost]
        public IActionResult CreateWorkout(CreateWorkoutDto dto)
        {
            var workout = new Workout
            {
                UserId = dto.UserId,
                WorkoutName = dto.WorkoutName,
                WorkoutDate = dto.WorkoutDate,
                Notes = dto.Notes
            };

            _context.Workouts.Add(workout);
            _context.SaveChanges();

            return Ok(new { message = "Workout created successfully", id = workout.Id });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWorkout(int id, UpdateWorkoutDto dto)
        {
            var workout = _context.Workouts.Find(id);

            if (workout == null)
            {
                return NotFound(new { message = "Workout not found" });
            }

            workout.WorkoutName = dto.WorkoutName;
            workout.WorkoutDate = dto.WorkoutDate;
            workout.Notes = dto.Notes;

            _context.SaveChanges();

            return Ok(new { message = "Workout updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorkout(int id)
        {
            var workout = _context.Workouts.Find(id);

            if (workout == null)
            {
                return NotFound(new { message = "Workout not found" });
            }

            _context.Workouts.Remove(workout);
            _context.SaveChanges();

            return Ok(new { message = "Workout deleted successfully" });
        }
    }
}
