using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Shopping.App.DAL;
using Shopping.App.Service;
using Shopping.App.ViewModel;
using Shopping.App.Views;
using System.Reflection;

namespace Shopping.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Material-Design-Iconic-Font.ttf", "MaterialDesignIconicFont");
			});

        string dbPath = InitDatabasePath("appDatabase.db3");

        builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite($"Data Source={dbPath}"));

        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<ProductService>();
		builder.Services.AddSingleton<OrderService>();
		builder.Services.AddSingleton<ShoppingCartService>();

		RegisterPages(builder);

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	private static string InitDatabasePath(string dbfile)
	{
		string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbfile);

		if (File.Exists(dbPath))
		{
			if (!Preferences.Get("InitData", false))
				return dbPath;

			File.Delete(dbPath);
			var dbShmPath = $"{dbPath}-shm";
			if (File.Exists(dbShmPath))
				File.Delete(dbPath);

			var dbWalPath = $"{dbPath}-wal";
			if(File.Exists(dbWalPath))
				File.Delete(dbWalPath);
        }

		var assembly = typeof(App).GetTypeInfo().Assembly;
        var appNamespace = typeof(App).Namespace;
        using Stream stream = assembly.GetManifestResourceStream($"{appNamespace}.Resources.Data.{dbfile}");
		using var memoryStream = new MemoryStream();

        stream.CopyTo(memoryStream);
        var dbBytes = memoryStream.ToArray();
        File.WriteAllBytes(dbPath, dbBytes);

		return dbPath;
    }


	public static void RegisterPages(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<HomeViewModel>();
		builder.Services.AddSingleton<HomePage>();

		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<LoginPage>();

		builder.Services.AddTransient<SignInViewModel>();
		builder.Services.AddTransient<SignInPage>();

        builder.Services.AddTransient<ProductViewModel>();
		builder.Services.AddTransient<ProductPage>();

        builder.Services.AddTransient<ShoppingCartViewModel>();
		builder.Services.AddTransient<ShoppingCartPage>();

        builder.Services.AddTransient<OrdersViewModel>();
		builder.Services.AddTransient<OrdersPage>();

		builder.Services.AddTransient<OrderViewModel>();
		builder.Services.AddTransient<OrderPage>();

		builder.Services.AddSingleton<UserViewModel>();
		builder.Services.AddSingleton<UserPage>();
	}
}
