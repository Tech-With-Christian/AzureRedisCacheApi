using AzureRedisCacheApi.Helpers;
using AzureRedisCacheApi.Persistence;
using AzureRedisCacheApi.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AzureRedisCacheApi.Services
{
	public static class Startup
	{
		/// <summary>
		/// Add Services to service container.
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		/// <param name="configuration">IConfiguration</param>
		/// <returns>IServiceCollection</returns>
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDatabase(configuration);
			services.AddCaching(configuration);
			services.AddTransient<IBookRepository, BookRepository>();
			services.AddTransient<IRedisCacheHelper, RedisCacheHelper>();
			services.AddTransient<IBookService, BookService>();
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			return services;
		}

		/// <summary>
		/// Add Database service to application. Will use a MS SQL Server.
		/// Please specify the connection string "Default" in appsettings.json for your database.
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		/// <param name="configuration">IConfiguration</param>
		/// <returns>IServiceCollection</returns>
		public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("Default")));

			return services;
		}

		/// <summary>
		/// Add Distributed Azure Redis Caching to Application.
		/// Please add AzureRedisUrl in appsettings.json under connection strings with the primary connection string from
		/// your Azure for Redis Cache Access Keys section.
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		/// <param name="configuration">IConfiguration</param>
		/// <returns>IServiceCollection</returns>
		public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = configuration.GetConnectionString("AzureRedisUrl");
				options.InstanceName = "master";
			});
			return services;
		}

		/// <summary>
		/// Add Application Services.
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		/// <returns>IApplicationBuilder</returns>
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
