using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Noter.Database.SqlLite;
using Noter.UseCases;
using Noter.UseCases.DatabaseInterfaces;
using Noter.UseCases.UseCaseInterfaces;

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
			//-----------------------------
			// Database Setup //
			// ----------------------------

			// Setup the connection to the Sqllite data source
			builder.Services.AddDbContext<NoterDBContext>(options =>
			{
				string path = Constants.DatabasePath;
				Directory.CreateDirectory(Path.GetDirectoryName(path)!); 
				options.UseSqlite($"Data Source={path}");
			});

			//-----------------------------
			// Use Case Setup //
			// ----------------------------
			builder.Services.AddSingleton<INoterDataStoreRepository, SqlLiteRepository>();
			builder.Services.AddSingleton<IViewNotesUseCase, ViewNotesUseCase>();

			//-----------------------------
			// Setup the database migration //
			// ----------------------------
			builder.Services.AddSingleton<DatabaseMigrationService>();

			return builder.Build();
		}
    }
}
