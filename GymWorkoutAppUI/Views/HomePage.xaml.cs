using System.Text.Json;
using GymWorkoutAppUI.Models;
using GymWorkoutAppUI.Services;

namespace GymWorkoutAppUI.Views;

public partial class HomePage : ContentPage
{
    private readonly HttpClient _httpClient;

    public HomePage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();

        GreetingLabel.Text = $"Hey, {UserSession.Username}!";
        DateLabel.Text = DateTime.Now.ToString("dddd, MMMM dd");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadStats();
    }

    private async void LoadStats()
    {
        try
        {
            var workoutResponse = await _httpClient.GetStringAsync(
                $"{ApiConfig.WorkoutsEndpoint}?userId={UserSession.UserId}");
            var workouts = JsonSerializer.Deserialize<List<Workout>>(
                workoutResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            WorkoutCountLabel.Text = (workouts?.Count ?? 0).ToString();

            var logResponse = await _httpClient.GetStringAsync(
                $"{ApiConfig.ProgressEndpoint}?userId={UserSession.UserId}");
            var logs = JsonSerializer.Deserialize<List<ProgressLog>>(
                logResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            LogCountLabel.Text = (logs?.Count ?? 0).ToString();

            var latestLog = logs?.OrderByDescending(l => l.LogDate).FirstOrDefault();
            WeightLabel.Text = latestLog?.BodyWeight?.ToString("0.#") ?? "—";
        }
        catch
        {

        }
    }

    private async void OnWorkoutListTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new WorkoutPage());
    }

    private async void OnAddWorkoutTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new AddWorkoutPage());
    }

    private async void OnProgressTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new ProgressPage());
    }

    private async void OnProfileTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Log Out", "Are you sure you want to log out?", "Yes", "Cancel");
        if (!confirm) return;

        UserSession.Clear();
        Application.Current!.MainPage = new NavigationPage(new LoginPage());
    }
}
