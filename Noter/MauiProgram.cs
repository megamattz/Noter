using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Noter.Database.SqlLite;
using Noter.UseCases;
using Noter.UseCases.DatabaseInterfaces;
using Noter.UseCases.UseCaseInterfaces;
using Noter.ViewModels;
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
			builder.Services.AddSingleton<IAddNoteUseCase, AddNoteUseCase>();
			builder.Services.AddSingleton<IViewNoteUseCase, ViewNoteUseCase>();
			builder.Services.AddSingleton<IEditNoteUseCase, EditNoteUseCase>();

			//-----------------------------
			// Navigation Setup //
			// ----------------------------
			builder.Services.AddSingleton<NotesPage>();
			builder.Services.AddSingleton<AddNotePage>();
			builder.Services.AddSingleton<ViewNotePage>();

			// ----------------------------
			// View Model Setup //
			// ----------------------------
			builder.Services.AddSingleton<NotesPageViewModel>();
			builder.Services.AddSingleton<AddEditNotePageViewModel>();
			builder.Services.AddSingleton<ViewNoteViewModel>();

			//-----------------------------
			// Setup the database migration //
			// ----------------------------
			builder.Services.AddSingleton<DatabaseMigrationService>();

			return builder.Build();
		}
    }
}
