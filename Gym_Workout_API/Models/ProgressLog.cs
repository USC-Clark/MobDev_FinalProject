namespace GymWorkoutApi.Models
{
    public class ProgressLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal? BodyWeight { get; set; }

        public DateTime LogDate { get; set; }

        public string? Notes { get; set; }

        public User? User { get; set; }
    }
}
