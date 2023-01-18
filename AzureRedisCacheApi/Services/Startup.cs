using AzureRedisCacheApi.Helpers;
using AzureRedisCacheApi.Persistence;
using AzureRedisCacheApi.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AzureRedisCacheApi.Services
{
	public static class Startup
	{
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

		public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("Default")));

			return services;
		}

		public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = configuration.GetConnectionString("AzureRedisUrl");
				options.InstanceName = "master";
			});
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
