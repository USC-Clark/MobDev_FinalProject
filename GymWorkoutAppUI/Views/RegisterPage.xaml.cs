using System.Text;
using System.Text.Json;
using GymWorkoutAppUI.Services;

namespace GymWorkoutAppUI.Views;

public partial class RegisterPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public RegisterPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        MessageLabel.IsVisible = false;

        if (string.IsNullOrWhiteSpace(UsernameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
            string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
        {
            ShowMessage("Please fill in all fields.", Colors.OrangeRed);
            return;
        }

        if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            ShowMessage("Passwords do not match.", Colors.OrangeRed);
            return;
        }

        if (PasswordEntry.Text.Length < 6)
        {
            ShowMessage("Password must be at least 6 characters.", Colors.OrangeRed);
            return;
        }

        RegisterButton.IsEnabled = false;
        RegisterButton.Text = "CREATING...";

        var registerData = new
        {
            username = UsernameEntry.Text.Trim(),
            email = EmailEntry.Text.Trim(),
            password = PasswordEntry.Text
        };

        var json = JsonSerializer.Serialize(registerData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(ApiConfig.RegisterEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Account created! Please log in.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<JsonElement>(body);
                string message = result.TryGetProperty("message", out var msg)
                    ? msg.GetString() ?? "Registration failed."
                    : "Registration failed.";
                ShowMessage(message, Colors.OrangeRed);
            }
        }
        catch (Exception ex)
        {
            ShowMessage($"Connection error: {ex.Message}", Colors.OrangeRed);
        }
        finally
        {
            RegisterButton.IsEnabled = true;
            RegisterButton.Text = "CREATE ACCOUNT";
        }
    }

    private async void OnBackToLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void ShowMessage(string message, Color color)
    {
        MessageLabel.Text = message;
        MessageLabel.TextColor = color;
        MessageLabel.IsVisible = true;
    }
}
