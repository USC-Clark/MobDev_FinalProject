namespace GymWorkoutAppUI.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string WorkoutName { get; set; } = string.Empty;
        public DateTime WorkoutDate { get; set; }
        public string? Notes { get; set; }
        public string FormattedDate => WorkoutDate.ToString("MMM dd, yyyy");
    }
}

namespace GymWorkoutAppUI.Models
{
    public class ProgressLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal? BodyWeight { get; set; }
        public DateTime LogDate { get; set; }
        public string? Notes { get; set; }

        public string FormattedDate => LogDate.ToString("MMM dd, yyyy");
        public string WeightDisplay => BodyWeight.HasValue ? $"{BodyWeight:0.##} kg" : "—";
    }
}
