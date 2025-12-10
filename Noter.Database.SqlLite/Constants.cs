namespace Noter.Database.SqlLite
{
	public static class Constants
	{
		public const string DatabaseFilename = "NoterSqlLite.db3";

		// This property is now lazy and safe at design time
		public static string DatabasePath => GetDatabasePath();

		private static string GetDatabasePath()
		{
			// If we are running inside MAUI (or the console host), use the path on the device
			if (AppDomain.CurrentDomain.FriendlyName.Contains("Noter") ||   // MAUI app
				AppDomain.CurrentDomain.FriendlyName.Contains("EFCoreHost")) // our host
			{
				return Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
			}

			// If we are running the migration scripts, then use the app data on the developers machine
			var fallback = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				"Noter",
				DatabaseFilename);

			Directory.CreateDirectory(Path.GetDirectoryName(fallback)!);
			return fallback;
		}
	}
}