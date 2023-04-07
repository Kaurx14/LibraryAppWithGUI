namespace LibraryAppWithGUI;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    async void NavigateTo(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}


