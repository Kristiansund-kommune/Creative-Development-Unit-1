using System.Text.Json.Serialization;
using Core.Concepts.Repository;
using Core.Concepts.Repository.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StationsContext>(async options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBikeStationRepository, BikeStationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRidesDbRepository, RidesDbRepository>();
builder.Services.AddScoped<IUserStatsRepository, UserStatsRepository>();


builder.Services.AddControllers().AddJsonOptions(x =>
	x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<StationsContext>();
	context.Database.EnsureCreated();
	DbInitializer.Initialize(context);
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
