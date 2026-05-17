using System.Text.Json;
using GymWorkoutAppUI.Models;
using GymWorkoutAppUI.Services;

namespace GymWorkoutAppUI.Views;

public partial class WorkoutPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public WorkoutPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadWorkouts();
    }

    private async void LoadWorkouts()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(
                $"{ApiConfig.WorkoutsEndpoint}?userId={UserSession.UserId}");

            var workouts = JsonSerializer.Deserialize<List<Workout>>(
                response,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            WorkoutCollectionView.ItemsSource = workouts;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load workouts.\n{ex.Message}", "OK");
        }
        finally
        {
            RefreshView.IsRefreshing = false;
        }
    }

    private void OnRefreshing(object sender, EventArgs e)
    {
        LoadWorkouts();
    }

    private async void OnWorkoutTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Workout workout)
        {
            await Navigation.PushAsync(new EditWorkoutPage(workout));
        }
    }

    private async void OnAddWorkoutClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddWorkoutPage());
    }
}
