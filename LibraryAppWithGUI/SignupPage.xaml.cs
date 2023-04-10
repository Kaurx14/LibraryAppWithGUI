using System.Data.SQLite;
using System.Diagnostics;

namespace LibraryAppWithGUI;

public partial class SignupPage : ContentPage
{
    private SQLiteConnection dbConnection;

    public SignupPage()
    {
        InitializeComponent();

        dbConnection = LoginPage.dbConnection;
        //dbConnection.CreateTable<User>();
    }

    async void OnSignupBtnClicked(object sender, EventArgs e)
    {
        dbConnection.Open();

        string email = emailEntry.Text;
        string firstName = firstNameEntry.Text;
        string lastName = lastNameEntry.Text;
        string password = passwordEntry.Text;

        // Add the new user to the database
        using (var command = new SQLiteCommand("INSERT INTO Users (Email, FirstName, LastName, Password) VALUES (@Email, @FirstName, @LastName, @Password)",
                   dbConnection))
        {
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Password", password);
            // To check if INSERT statement was successful or not. If was successful, number of rows affected will be greater than 0
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                // Sign up successful
                await DisplayAlert("Sign up Successful!", "You were signed up successfully!", "OK");
                // Navigate to the login page
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                // Signup failed, username already exists
                DisplayAlert("Error", "Email already exists", "OK");
            }
        }

        //// Check if the username already exists in the database
        //if (dbConnection.Table<User>().FirstOrDefault(u => u.Email == email) != null)
        //{
        //    DisplayAlert("Error", "Username already exists", "OK");
        //}
        //else
        //{
        //    // Create a new user object with the entered details
        //    var user = new User
        //    {
        //        Username = username,
        //        Password = password
        //    };

        //    // Insert the user into the database
        //    int rowsAffected = dbConnection.Insert(user);
        //    if (rowsAffected > 0)
        //    {
        //        DisplayAlert("Success", "User account created", "OK");

        //        // Navigate back to the login page
        //        Navigation.PopAsync();
        //    }
        //    else
        //    {
        //        DisplayAlert("Error", "Unable to create user account", "OK");
        //    }
        //}
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        dbConnection.Close();
    }
}