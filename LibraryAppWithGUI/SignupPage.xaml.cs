using System.Data.SQLite;
using System.Diagnostics;

namespace LibraryAppWithGUI;

public partial class SignupPage : ContentPage
{
    private SQLiteConnection dbConnection;

    public SignupPage()
    {
        InitializeComponent();

        dbConnection = new SQLiteConnection(AppManager.connectionString);
    }

    async void OnSignupBtnClicked(object sender, EventArgs e)
    {
        string email = emailEntry.Text;
        string firstName = firstNameEntry.Text;
        string lastName = lastNameEntry.Text;
        string password = passwordEntry.Text;

        dbConnection.Open();

        var checkUserCommand = new SQLiteCommand("SELECT COUNT (*) FROM Users WHERE Email = @Email", dbConnection);
        checkUserCommand.Parameters.AddWithValue("@Email", email);
        int rowsAffected = Convert.ToInt32(checkUserCommand.ExecuteScalar());

        if (!AppManager.userDatabase.AreValidFields(email, firstName, lastName, password))
        {
            await DisplayAlert("Error", "Please fill in all the fields correctly", "OK");
        }
        else
        {
            // Check if the user exists in the database
            if (rowsAffected > 0)
            {
                // Have to refresh the page or smth because otherwise the database is broken!

                await DisplayAlert("Try again!", "User Already exists", "OK");
            }
            else
            {
                // Add the new user to the database
                using (var command = new SQLiteCommand(
                           "INSERT INTO Users (Email, FirstName, LastName, Password) VALUES (@Email, @FirstName, @LastName, @Password)",
                           dbConnection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Password", password);
                    // To check if INSERT statement was successful or not. If was successful, number of rows affected will be greater than 0
                    command.ExecuteNonQuery();

                    // Sign up successful
                    await DisplayAlert("Sign up Successful!", "yep", "OK");
                    // Navigate to the login page
                    Navigation.PushAsync(new MainPage());
                }
            }
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        dbConnection.Close();
    }
}