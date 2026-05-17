namespace GymWorkoutAppUI.Services
{
    public static class UserSession
    {
        public static int UserId { get; private set; }
        public static string Username { get; private set; } = string.Empty;
        public static string Email { get; private set; } = string.Empty;
        public static bool IsLoggedIn => UserId > 0;

        public static void SetUser(int id, string username, string email)
        {
            UserId = id;
            Username = username;
            Email = email;
        }

        public static void Clear()
        {
            UserId = 0;
            Username = string.Empty;
            Email = string.Empty;
        }
    }
}
