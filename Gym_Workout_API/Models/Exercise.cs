namespace GymWorkoutApi.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public string ExerciseName { get; set; } = string.Empty;

        public string? MuscleGroup { get; set; }

        public string? Description { get; set; }
    }
}
