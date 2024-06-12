using Microsoft.Extensions.Logging;

namespace MauiClient
{
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
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<CountryViewModel>();
            builder.Services.AddSingleton<CountryPage>();
            builder.Services.AddSingleton<SettlementViewModel>();
            builder.Services.AddSingleton<SettlementPage>();
            builder.Services.AddSingleton<CitizenViewModel>();
            builder.Services.AddSingleton<CitizenPage>();
            builder.Services.AddSingleton<NonCrudDevelopedCriminalsViewModel>();
            builder.Services.AddSingleton<NonCrudDevelopedCriminals>();
            builder.Services.AddSingleton<NonCrudPoorOldPeopleViewModel>();
            builder.Services.AddSingleton<NonCrudPoorOldPeople>();
            return builder.Build();
        }
    }
}
