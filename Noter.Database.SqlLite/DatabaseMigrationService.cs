using Microsoft.EntityFrameworkCore;

namespace Noter.Database.SqlLite
{
	public class DatabaseMigrationService
	{

		NoterDBContext _dbContext;

		public DatabaseMigrationService(NoterDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void RunMigrations()
		{
			_dbContext.Database.Migrate();
		}
	}
}
