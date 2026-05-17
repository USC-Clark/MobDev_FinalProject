using GymWorkoutAppUI.Services;

namespace GymWorkoutAppUI.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        string username = UserSession.Username;
        string email = UserSession.Email;
        int userId = UserSession.UserId;

        AvatarLabel.Text = username.Length > 0 ? username[0].ToString().ToUpper() : "?";

        UsernameLabel.Text = username;
        EmailLabel.Text = email;

        UsernameCellLabel.Text = username;
        EmailCellLabel.Text = email;
        UserIdLabel.Text = $"#{userId}";
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Log Out", "Are you sure?", "Yes", "Cancel");
        if (!confirm) return;

        UserSession.Clear();
        Application.Current!.MainPage = new NavigationPage(new LoginPage());
    }
}
