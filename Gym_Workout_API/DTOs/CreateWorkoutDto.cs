namespace GymWorkoutApi.DTOs
{
    public class CreateWorkoutDto
    {
        public int UserId { get; set; }
        public string WorkoutName { get; set; } = string.Empty;
        public DateTime WorkoutDate { get; set; }
        public string? Notes { get; set; }
    }
}
