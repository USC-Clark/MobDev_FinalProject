using System.Text;
using System.Text.Json;
using GymWorkoutAppUI.Services;

namespace GymWorkoutAppUI.Views;

public partial class AddWorkoutPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public AddWorkoutPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        WorkoutDatePicker.Date = DateTime.Today;
    }

    private async void OnSaveWorkoutClicked(object sender, EventArgs e)
    {
        MessageLabel.IsVisible = false;

        if (string.IsNullOrWhiteSpace(WorkoutNameEntry.Text))
        {
            ShowMessage("Please enter a workout name.", Colors.OrangeRed);
            return;
        }

        SaveButton.IsEnabled = false;
        SaveButton.Text = "SAVING...";

        var workoutData = new
        {
            userId = UserSession.UserId,
            workoutName = WorkoutNameEntry.Text.Trim(),
            workoutDate = WorkoutDatePicker.Date,
            notes = NotesEditor.Text?.Trim()
        };

        var json = JsonSerializer.Serialize(workoutData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(ApiConfig.WorkoutsEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                ShowMessage("Workout saved! 💪", Colors.LimeGreen);
                WorkoutNameEntry.Text = string.Empty;
                NotesEditor.Text = string.Empty;
                WorkoutDatePicker.Date = DateTime.Today;

                await Task.Delay(1000);
                await Navigation.PopAsync();
            }
            else
            {
                ShowMessage("Failed to save workout. Try again.", Colors.OrangeRed);
            }
        }
        catch (Exception ex)
        {
            ShowMessage($"Error: {ex.Message}", Colors.OrangeRed);
        }
        finally
        {
            SaveButton.IsEnabled = true;
            SaveButton.Text = "SAVE WORKOUT";
        }
    }

    private void ShowMessage(string message, Color color)
    {
        MessageLabel.Text = message;
        MessageLabel.TextColor = color;
        MessageLabel.IsVisible = true;
    }
}
