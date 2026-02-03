using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Noter.Database.SqlLite;
using Noter.UseCases;
using Noter.UseCases.DatabaseInterfaces;
using Noter.UseCases.UseCaseInterfaces;
using Noter.ViewModels;
using Noter.Views;
using Microsoft.Maui.Handlers;
using Noter.Views.Popups;

namespace Noter
{
	public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
			MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
				.UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

			// These are needed becuase .NET Maiu has some default behaviours and padding that cannot be changed via xaml directly
			// and interfere with how I want the layout to look. 
			RemoveUnderlineOnEntry();
			RemoveAutomaticPaddingForEditor();
			RemoveAutomaticPadding();
			CenterEntryTextVertically();
			RemoveCheckboxPadding();
			TightenRadioButtonPadding();


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
			builder.Services.AddSingleton<IDeleteNoteUseCase, DeleteNoteUseCase>();
			builder.Services.AddSingleton<ICountNotesUseCase, CountNotesUseCase>();

			//-----------------------------
			// Navigation Setup //
			// ----------------------------
			builder.Services.AddSingleton<NotesPage>();
			builder.Services.AddSingleton<AddEditNotePage>();
			builder.Services.AddSingleton<ViewNotePage>();
			builder.Services.AddSingleton<AboutPopup>();

			// ----------------------------
			// View Model Setup //
			// ----------------------------
			builder.Services.AddSingleton<NotesPageViewModel>();
			builder.Services.AddSingleton<AddEditNotePageViewModel>();
			builder.Services.AddSingleton<ViewNoteViewModel>();
			builder.Services.AddSingleton<AboutPopupViewModel>();
			builder.Services.AddSingleton<FilterAndSortViewModel>();

			//-----------------------------
			// Setup the database migration //
			// ----------------------------
			builder.Services.AddSingleton<DatabaseMigrationService>();

			return builder.Build();
		}

		private static void RemoveCheckboxPadding()
		{
#if ANDROID
    Microsoft.Maui.Handlers.CheckBoxHandler.Mapper.AppendToMapping(
        "RemoveCheckboxPadding",
        (handler, view) =>
        {
            if (handler.PlatformView is Android.Widget.CheckBox nativeCheckBox)
            {
                nativeCheckBox.CompoundDrawablePadding = 0;

                // Extra aggressive zeroing
                nativeCheckBox.SetPadding(0, 0, 0, 0);
                nativeCheckBox.SetPaddingRelative(0, 0, 0, 0);            

                nativeCheckBox.Gravity = Android.Views.GravityFlags.CenterVertical |
                                         Android.Views.GravityFlags.Start;

                nativeCheckBox.RequestLayout();
            }
        });
#endif
		}

		private static void TightenRadioButtonPadding()
		{
#if ANDROID
    Microsoft.Maui.Handlers.RadioButtonHandler.Mapper.AppendToMapping(
        "TightRadioSpacing",
        (handler, view) =>
        {
            if (handler.PlatformView is Android.Widget.RadioButton nativeRadio)
            {
                nativeRadio.CompoundDrawablePadding = 0;   // ← reduces gap between circle & text
                nativeRadio.SetPadding(0, 0, 0, 0);
                nativeRadio.SetPaddingRelative(0, 0, 0, 0);
                nativeRadio.Gravity = Android.Views.GravityFlags.CenterVertical | Android.Views.GravityFlags.Start;
            }
        });
#endif
		}

		private static void CenterEntryTextVertically()
		{
#if ANDROID
    Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("CenterVertically", (handler, view) =>
    {
        if (handler.PlatformView is Android.Widget.EditText editText)
        {
            editText.Gravity = Android.Views.GravityFlags.CenterVertical | Android.Views.GravityFlags.Start;
            // GravityFlags.Start keeps text left-aligned (normal behavior)
            // If you want the text also horizontally centered: use GravityFlags.Center instead
        }
    });
#endif
		}

		private static void RemoveAutomaticPadding()
		{
			// Android automatically puts in some padding which messes up the layout. 
			// Remove it so I have more control
			Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoPadding", (handler, view) =>
			{
#if ANDROID
				if (handler.PlatformView is Android.Widget.EditText editText)
				{
					editText.SetPadding(0, 0, 0, 0);
					editText.Gravity = Android.Views.GravityFlags.Start | Android.Views.GravityFlags.Top;
					editText.CompoundDrawablePadding = 0;
				}
#endif
			});
		}

		private static void RemoveAutomaticPaddingForEditor()
		{
			Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("NoPaddingEditor", (handler, view) =>
			{
#if ANDROID
				if (handler.PlatformView is Android.Widget.EditText editText)
				{
					editText.SetPadding(0, 0, 0, 0);
					editText.Gravity = Android.Views.GravityFlags.Start | Android.Views.GravityFlags.Top;
					editText.CompoundDrawablePadding = 0;

					// Optional but often helpful for Editors (reduces extra top "air" from font metrics)
					editText.SetIncludeFontPadding(false);
				}
#endif
			});
		}

		private static void RemoveUnderlineOnEntry()
		{
			// By default we get an ugly underline on the Text Entry controls. This code removves it
			Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
			{
#if ANDROID
        handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif

#if IOS || MACCATALYST
				handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
				handler.PlatformView.Layer.BorderWidth = 0;
#endif
			});

			Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
			{
#if ANDROID
        handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif

#if IOS || MACCATALYST
				handler.PlatformView.Layer.BorderWidth = 0;
#endif
			});
		}
	}	
}
