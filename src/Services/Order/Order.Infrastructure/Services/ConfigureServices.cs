using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Services
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<OrderContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"),
					builder => builder.MigrationsAssembly(typeof(OrderContext).Assembly.FullName));
			});

			services.AddScoped<OrderSeeder>();

			return services;
		}
	}
}
