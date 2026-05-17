using GymWorkoutAppUI.Models;
using GymWorkoutAppUI.Services;
using System.Text;
using System.Text.Json;

namespace GymWorkoutAppUI.Views;

public partial class EditWorkoutPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private readonly Workout _workout;

    public EditWorkoutPage(Workout workout)
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        _workout = workout;

        WorkoutNameEntry.Text = workout.WorkoutName;
        WorkoutDatePicker.Date = workout.WorkoutDate;
        NotesEditor.Text = workout.Notes;
    }

    private async void OnUpdateWorkoutClicked(object sender, EventArgs e)
    {
        MessageLabel.IsVisible = false;

        if (string.IsNullOrWhiteSpace(WorkoutNameEntry.Text))
        {
            ShowMessage("Workout name cannot be empty.", Colors.OrangeRed);
            return;
        }

        UpdateButton.IsEnabled = false;
        UpdateButton.Text = "UPDATING...";

        var updatedData = new
        {
            workoutName = WorkoutNameEntry.Text.Trim(),
            workoutDate = WorkoutDatePicker.Date,
            notes = NotesEditor.Text?.Trim()
        };

        var json = JsonSerializer.Serialize(updatedData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PutAsync(
                $"{ApiConfig.WorkoutsEndpoint}/{_workout.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Workout updated!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                ShowMessage("Update failed. Try again.", Colors.OrangeRed);
            }
        }
        catch (Exception ex)
        {
            ShowMessage($"Error: {ex.Message}", Colors.OrangeRed);
        }
        finally
        {
            UpdateButton.IsEnabled = true;
            UpdateButton.Text = "UPDATE WORKOUT";
        }
    }

    private async void OnDeleteWorkoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Delete Workout",
            $"Delete \"{_workout.WorkoutName}\"? This cannot be undone.",
            "Delete",
            "Cancel");

        if (!confirm) return;

        DeleteButton.IsEnabled = false;

        try
        {
            var response = await _httpClient.DeleteAsync(
                $"{ApiConfig.WorkoutsEndpoint}/{_workout.Id}");

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Deleted", "Workout removed.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Delete failed. Try again.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            DeleteButton.IsEnabled = true;
        }
    }

    private void ShowMessage(string message, Color color)
    {
        MessageLabel.Text = message;
        MessageLabel.TextColor = color;
        MessageLabel.IsVisible = true;
    }
}
