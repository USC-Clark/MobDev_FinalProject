using System.Text;
using System.Text.Json;
using GymWorkoutAppUI.Models;
using GymWorkoutAppUI.Services;

namespace GymWorkoutAppUI.Views;

public partial class ProgressPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public ProgressPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        LogDatePicker.Date = DateTime.Today;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadLogs();
    }

    private async void LoadLogs()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(
                $"{ApiConfig.ProgressEndpoint}?userId={UserSession.UserId}");

            var logs = JsonSerializer.Deserialize<List<ProgressLog>>(
                response,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            LogCollectionView.ItemsSource = logs;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load logs.\n{ex.Message}", "OK");
        }
        finally
        {
            RefreshView.IsRefreshing = false;
        }
    }

    private void OnRefreshing(object sender, EventArgs e)
    {
        LoadLogs();
    }

    private async void OnAddLogClicked(object sender, EventArgs e)
    {
        LogMessageLabel.IsVisible = false;

        if (string.IsNullOrWhiteSpace(WeightEntry.Text) ||
            !decimal.TryParse(WeightEntry.Text, out decimal weight))
        {
            ShowMessage("Please enter a valid weight.", Colors.OrangeRed);
            return;
        }

        var logData = new
        {
            userId = UserSession.UserId,
            bodyWeight = weight,
            logDate = LogDatePicker.Date,
            notes = LogNotesEntry.Text?.Trim()
        };

        var json = JsonSerializer.Serialize(logData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(ApiConfig.ProgressEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                ShowMessage("Log added!", Colors.LimeGreen);
                WeightEntry.Text = string.Empty;
                LogNotesEntry.Text = string.Empty;
                LogDatePicker.Date = DateTime.Today;
                LoadLogs();
            }
            else
            {
                ShowMessage("Failed to add log.", Colors.OrangeRed);
            }
        }
        catch (Exception ex)
        {
            ShowMessage($"Error: {ex.Message}", Colors.OrangeRed);
        }
    }

    private void ShowMessage(string message, Color color)
    {
        LogMessageLabel.Text = message;
        LogMessageLabel.TextColor = color;
        LogMessageLabel.IsVisible = true;
    }
}
