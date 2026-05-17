using System.Text;
using System.Text.Json;
using GymWorkoutAppUI.Services;

namespace GymWorkoutAppUI.Views;

public partial class LoginPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public LoginPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        MessageLabel.IsVisible = false;

        if (string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            ShowError("Please enter your email and password.");
            return;
        }

        LoginButton.IsEnabled = false;
        LoginButton.Text = "SIGNING IN...";

        var loginData = new
        {
            email = EmailEntry.Text.Trim(),
            password = PasswordEntry.Text
        };

        var json = JsonSerializer.Serialize(loginData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(ApiConfig.LoginEndpoint, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<JsonElement>(responseBody);
                int userId = result.GetProperty("id").GetInt32();
                string username = result.GetProperty("username").GetString() ?? "";
                string email = result.GetProperty("email").GetString() ?? "";

                UserSession.SetUser(userId, username, email);

                Application.Current!.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                ShowError("Invalid email or password.");
            }
        }
        catch (Exception ex)
        {
            ShowError($"Connection error.\nCheck your IP in ApiConfig.cs\n({ex.Message})");
        }
        finally
        {
            LoginButton.IsEnabled = true;
            LoginButton.Text = "LOG IN";
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    private void ShowError(string message)
    {
        MessageLabel.Text = message;
        MessageLabel.IsVisible = true;
    }
}
