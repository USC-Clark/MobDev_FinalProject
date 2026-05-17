namespace GymWorkoutApi.DTOs
{
    public class UpdateWorkoutDto
    {
        public string WorkoutName { get; set; } = string.Empty;
        public DateTime WorkoutDate { get; set; }
        public string? Notes { get; set; }
    }
}
