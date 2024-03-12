using Microsoft.EntityFrameworkCore;
using Core.Concepts.Entities;

public class StationsContext : DbContext
{
	public StationsContext(DbContextOptions<StationsContext> options) : base(options)
	{
	}

	public DbSet<BikeStation> BikeStations { get; set; }
	public DbSet<Bike> Bikes { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<UserLevel> Levels { get; set; }
	public DbSet<BikeStationDock> Docks { get; set; }
	public DbSet<Route> Routes { get; set; }



	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<BikeStation>().ToTable("BikeStation");
		modelBuilder.Entity<Bike>().ToTable("Bike");
		modelBuilder.Entity<User>().ToTable("Users");
		modelBuilder.Entity<UserLevel>().ToTable("Levels");
		modelBuilder.Entity<Route>().ToTable("Routes");
		modelBuilder.Entity<BikeStationDock>().ToTable("Docks");
	}
}
