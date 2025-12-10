using Microsoft.EntityFrameworkCore;
using Noter.Database.SqlLite;

// This is a host program for generating the EntityFramework DBMigration scripts. 
// It is needed since it EntityFramework has trouble generating then from within Noter.Database.SqlLite directly.

Console.WriteLine("EF Core Migration Host – starting...");

// 1. Build a ServiceCollection exactly like MauiProgram.cs does
ServiceCollection services = new ServiceCollection();

services.AddDbContext<NoterDBContext>(options =>
	options.UseSqlite($"Data Source={Constants.DatabasePath}"));

// 2. Create the service provider
IServiceProvider serviceProvider = services.BuildServiceProvider();

// 3. Create a scope and resolve the DbContext
using IServiceScope scope = serviceProvider.CreateScope();
NoterDBContext db = scope.ServiceProvider.GetRequiredService<NoterDBContext>();

// 4. Apply migrations
Console.WriteLine("Applying pending migrations...");
db.Database.Migrate();

// 5. Done!
Console.WriteLine();
Console.WriteLine("Database is ready!");
Console.WriteLine($"Location: {Constants.DatabasePath}");
Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();


