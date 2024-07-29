using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using DevExpress.Maui;
using FreightControlMaui.MVVM.ViewModels;
using FreightControlMaui.MVVM.Views;
using FreightControlMaui.Services.Authentication;
using FreightControlMaui.Services.Chart;
using FreightControlMaui.Services.Exportation;
using FreightControlMaui.Services.Navigation;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace FreightControlMaui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSkiaSharp()
            .UseDevExpress()
            .UseMicrocharts()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
            fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
            fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemiBold");          
        });

        RegisterServicesToViews(builder);
        //RegisterServicesToViewModels(builder);
        RegisterServicesToAppServices(builder);

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }

    private static void RegisterServicesToAppServices(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
        builder.Services.AddSingleton<IChartService, ChartService>();
        builder.Services.AddSingleton<IExportData, ExportData>();
    }

    private static void RegisterServicesToViewModels(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<FreightViewModel>();
        builder.Services.AddTransient<AddFreightViewModel>();
        builder.Services.AddTransient<DetailFreightViewModel>();
        builder.Services.AddTransient<ToFuelViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<ChartsViewModel>();
    }

    private static void RegisterServicesToViews(MauiAppBuilder builder)
    {
        builder.Services.AddTransient<HomeView>();
        builder.Services.AddTransient<FreightView>();
        builder.Services.AddTransient<AddFreightView>();
        builder.Services.AddTransient<DetailFreightView>();
        builder.Services.AddTransient<ToFuelView>();
        builder.Services.AddTransient<RegisterView>();
        builder.Services.AddTransient<EditUserView>();
        builder.Services.AddTransient<ChartsView>();
    }
}