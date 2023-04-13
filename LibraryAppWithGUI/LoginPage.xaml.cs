using System.Data.SQLite;
using System.Diagnostics;

namespace LibraryAppWithGUI;

public partial class LoginPage : ContentPage
{
    private SQLiteConnection dbConnection;
    public LoginPage()
    {
        InitializeComponent();

        dbConnection = new SQLiteConnection(AppManager.connectionString);
    }

    async void OnLoginBtnClicked(object sender, EventArgs e)
    {
        string email = emailEntry.Text;
        string password = passwordEntry.Text;
        dbConnection.Open();

        // Check if the user exists in the database
        using (var command = new SQLiteCommand("SELECT * FROM Users WHERE Email=@Email AND Password=@Password", dbConnection))
        {
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    // User exists, login successful
                    await DisplayAlert("Login Succesful!", "You were logged in succesfully!", "OK");
                    // Navigate to the main page
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    // User does not exist, show an error message
                    await DisplayAlert("Error", "Invalid username or password", "OK");
                }
            }
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        dbConnection.Close();
    }

    async void OnSignupBtnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignupPage());
    }
}
