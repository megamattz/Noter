using Microsoft.Extensions.Logging;
using Noter.Database.SqlLite;
using Noter.UseCases;
using Noter.UseCases.DatabaseInterfaces;
using Noter.UseCases.UseCaseInterfaces;
using Noter.Views;

namespace Noter
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
			builder.Services.AddSingleton<INoterDataStoreRepository, SqlLiteRepository>();
			builder.Services.AddSingleton<IViewNotesUseCase, ViewNotesUseCase>();

			return builder.Build();
        }
    }
}
