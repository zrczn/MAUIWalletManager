using MauiTransaction.Data;
using MauiTransaction.Httph;
using MauiTransaction.Views;
using Microsoft.Extensions.Logging;

namespace MauiTransaction
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

            builder.Services.AddTransient<IHttpCli, HttpCli>();
            builder.Services.AddTransient<ICRUDService, CRUDService>();
            builder.Services.AddTransient<MainMenu>();
            builder.Services.AddTransient<EditTransaction>();
            builder.Services.AddTransient<DetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            return app;
        }
    }
}
