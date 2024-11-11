using Contract.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Common.Interfaces;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repositories;

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
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

			return services;
		}
	}
}
