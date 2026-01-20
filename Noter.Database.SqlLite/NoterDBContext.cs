using Microsoft.EntityFrameworkCore;
using Noter.CoreBusiness;

namespace Noter.Database.SqlLite
{
	public class NoterDBContext : DbContext
	{
		public DbSet<Note> Notes => Set<Note>();

		public string DbPath { get; } = "";

		public NoterDBContext(DbContextOptions<NoterDBContext> options) : base(options)
		{
			//DbPath = Constants.DatabasePath;

			// Best to ensure the database directory exists to avoid potential issues
			//Directory.CreateDirectory(Constants.DatabaseDirectory);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (!options.IsConfigured)
			{
				options.UseSqlite($"Data Source={DbPath}");
			}
		}
	}
}
