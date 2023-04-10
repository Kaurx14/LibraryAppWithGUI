using System.Data.SQLite;
using System.Diagnostics;

namespace LibraryAppWithGUI;

public partial class LoginPage : ContentPage
{
    public static SQLiteConnection dbConnection;

    public static string dbFile = "LibraryDB.db";
    public static string solutionFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../../Resources/"));
    public static string dbFilePath = Path.Combine(solutionFolder, dbFile);

    public static string connectionString;

    public LoginPage()
    {
        InitializeComponent();

        BindingContext = this;
        // Initializes database connection
        Debug.WriteLine(dbFilePath);
        Debug.WriteLine(File.Exists(dbFilePath));

        if (File.Exists(dbFilePath))
        {
            connectionString = $"Data Source={dbFilePath}";
            dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
        }

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

    async void OnSignupBtnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignupPage());
    }
}
