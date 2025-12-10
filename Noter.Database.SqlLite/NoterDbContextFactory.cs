using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Noter.Database.SqlLite   
{
	public class NoterDbContextFactory : IDesignTimeDbContextFactory<NoterDBContext>
	{
		public NoterDBContext CreateDbContext(string[] args)
		{
			// This class is not used in the actual application, however it is required for Entity Framework 
			// in order to be able to create the migration scripts
			DbContextOptionsBuilder<NoterDBContext> optionsBuilder = new DbContextOptionsBuilder<NoterDBContext>();			

			optionsBuilder.UseSqlite($"Data Source={Constants.DatabasePath}");

			return new NoterDBContext(optionsBuilder.Options);
		}
	}
}