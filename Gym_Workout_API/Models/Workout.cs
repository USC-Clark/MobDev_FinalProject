namespace GymWorkoutApi.Models
{
    public class Workout
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string WorkoutName { get; set; } = string.Empty;

        public DateTime WorkoutDate { get; set; }

        public string? Notes { get; set; }

        public User? User { get; set; }
    }
}
