namespace GymWorkoutApi.DTOs
{
    public class UpdateProgressLogDto
    {
        public decimal? BodyWeight { get; set; }
        public DateTime LogDate { get; set; }
        public string? Notes { get; set; }
    }
}
