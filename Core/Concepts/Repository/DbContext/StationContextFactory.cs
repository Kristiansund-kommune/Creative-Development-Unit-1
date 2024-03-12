



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

public class StationContextFactory : IDesignTimeDbContextFactory<StationsContext>
{

	public StationsContext CreateDbContext(string[] args)
	{
		var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			.AddUserSecrets<StationContextFactory>(); // namespaces would need to match

		var configuration = builder.Build();


		var optionsBuilder = new DbContextOptionsBuilder<StationsContext>();

		optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		// YourConnectionStringName is your connection string key in the secrets

		return new StationsContext(optionsBuilder.Options);
	}
}
