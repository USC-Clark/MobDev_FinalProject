using GymWorkoutApi.Data;
using GymWorkoutApi.DTOs;
using GymWorkoutApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymWorkoutApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgressLogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProgressLogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllLogs([FromQuery] int? userId)
        {
            var query = _context.ProgressLogs.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(l => l.UserId == userId.Value);
            }

            var logs = query
                .OrderByDescending(l => l.LogDate)
                .Select(l => new
                {
                    l.Id,
                    l.UserId,
                    l.BodyWeight,
                    l.LogDate,
                    l.Notes
                })
                .ToList();

            return Ok(logs);
        }

        [HttpGet("{id}")]
        public IActionResult GetLogById(int id)
        {
            var log = _context.ProgressLogs
                .Where(l => l.Id == id)
                .Select(l => new
                {
                    l.Id,
                    l.UserId,
                    l.BodyWeight,
                    l.LogDate,
                    l.Notes
                })
                .FirstOrDefault();

            if (log == null)
            {
                return NotFound(new { message = "Progress log not found" });
            }

            return Ok(log);
        }

        [HttpPost]
        public IActionResult CreateLog(CreateProgressLogDto dto)
        {
            var log = new ProgressLog
            {
                UserId = dto.UserId,
                BodyWeight = dto.BodyWeight,
                LogDate = dto.LogDate,
                Notes = dto.Notes
            };

            _context.ProgressLogs.Add(log);
            _context.SaveChanges();

            return Ok(new { message = "Progress log created successfully", id = log.Id });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLog(int id, UpdateProgressLogDto dto)
        {
            var log = _context.ProgressLogs.Find(id);

            if (log == null)
            {
                return NotFound(new { message = "Progress log not found" });
            }

            log.BodyWeight = dto.BodyWeight;
            log.LogDate = dto.LogDate;
            log.Notes = dto.Notes;

            _context.SaveChanges();

            return Ok(new { message = "Progress log updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLog(int id)
        {
            var log = _context.ProgressLogs.Find(id);

            if (log == null)
            {
                return NotFound(new { message = "Progress log not found" });
            }

            _context.ProgressLogs.Remove(log);
            _context.SaveChanges();

            return Ok(new { message = "Progress log deleted successfully" });
        }
    }
}
