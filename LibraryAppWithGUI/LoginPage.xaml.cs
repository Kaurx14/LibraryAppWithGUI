using System.Data.SQLite;

namespace LibraryAppWithGUI;

public partial class LoginPage : ContentPage
{
    private SQLiteConnection dbConnection;
    public LoginPage()
    {
        InitializeComponent();

        BindingContext = this;
        // Initializes database connection
        // The solution that Lauri had, no connection currently
        dbConnection = new SQLiteConnection("Data Source=/Resources/Library.db;Version=3;");
        dbConnection.Open();
    }

    async void OnLoginBtnClicked(object sender, EventArgs e)
    {
        string email = emailEntry.Text;
        string password = passwordEntry.Text;

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

    // What is OnDisappearing() method here
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        dbConnection.Close();
    }
}
