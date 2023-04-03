using Shopping.App.Service;
using Shopping.App.Views;

namespace Shopping.App;

public partial class AppShell : Shell
{
	private readonly UserService _userService;

	public AppShell(UserService userService)
	{
		_userService = userService;
		InitializeComponent();

		RoutingRegister();
	}

	private void RoutingRegister()
	{
        Routing.RegisterRoute("Product/Detail", typeof(ProductPage));
        Routing.RegisterRoute("Order/Detail", typeof(OrderPage));
        Routing.RegisterRoute("Login", typeof(LoginPage));
        Routing.RegisterRoute("SignIn", typeof(SignInPage));

	}
}
