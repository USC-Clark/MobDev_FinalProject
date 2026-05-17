namespace GymWorkoutApi.DTOs
{
    public class CreateProgressLogDto
    {
        public int UserId { get; set; }
        public decimal? BodyWeight { get; set; }
        public DateTime LogDate { get; set; }
        public string? Notes { get; set; }
    }
}
