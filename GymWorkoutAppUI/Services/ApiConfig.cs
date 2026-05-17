namespace GymWorkoutAppUI.Services
{
    public static class ApiConfig
    {                                 //Change this to your PCs IP
        public const string BaseUrl = "http://192.168.1.59:5275";

        public const string LoginEndpoint      = $"{BaseUrl}/api/Auth/login";
        public const string RegisterEndpoint   = $"{BaseUrl}/api/Auth/register";
        public const string WorkoutsEndpoint   = $"{BaseUrl}/api/Workouts";
        public const string ProgressEndpoint   = $"{BaseUrl}/api/ProgressLogs";
    }
}
