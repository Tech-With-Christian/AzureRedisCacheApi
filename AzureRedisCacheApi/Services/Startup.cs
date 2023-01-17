using AzureRedisCacheApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzureRedisCacheApi.Services
{
	public static class Startup
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDatabase(configuration);
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			return services;
		}

		public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("Default")));

			return services;
		}

		public static IApplicationBuilder AddApplication(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			app.UseHttpsRedirection();
			app.UseAuthorization();

			return app;
		}
	}
}
