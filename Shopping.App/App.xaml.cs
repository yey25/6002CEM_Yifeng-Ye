using Shopping.App.Service;

namespace Shopping.App;

public partial class App : Application
{
    private readonly UserService _userService;

    public App(UserService userService)
	{
        _userService = userService;
		InitializeComponent();

		MainPage = new AppShell(userService);
	}

    protected override void OnStart()
    {
        base.OnStart();

        Login();
    }

    private async void Login()
    {
        _userService.ValidCashUser();
        if (_userService.IsLogIn)
            return;

        await Shell.Current.GoToAsync("Login");
    }
}
