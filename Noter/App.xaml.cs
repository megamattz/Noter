using Noter.Database.SqlLite;

namespace Noter
{
    public partial class App : Application
    {
        public App(DatabaseMigrationService databaseMigrationService)
        {
            InitializeComponent();

			// Make sure the database migrations for the sqllite DB on the user device get run during app startup
			databaseMigrationService.RunMigrations();
		}

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}