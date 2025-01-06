using Youth.ViewModels;

namespace Youth;

public static class ServiceProvider
{

    private static void RegisterViewModels(IServiceCollection services)
    {
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<HomeViewModel>();
        //services.AddTransient<WelcomeViewModel>();
    }

    public static TService GetService<TService>()
        => Current.GetService<TService>();

    public static IServiceProvider Current
        =>
#if WINDOWS10_0_17763_0_OR_GREATER
			MauiWinUIApplication.Current.Services;
#elif ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
            MauiUIApplicationDelegate.Current.Services;
#else
            null;
#endif
}
