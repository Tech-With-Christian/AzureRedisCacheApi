using AzureRedisCacheApi.Persistence;
using AzureRedisCacheApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
// Seed Database
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider
		.GetRequiredService<AppDbContext>();

	dbContext.Database.Migrate();
}

app.AddApplication();
app.MapControllers();
app.Run();
