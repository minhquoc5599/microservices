﻿using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product.Api.Data;

namespace Product.Api.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddControllers();
			services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			services.ConfigureProductDbContext(configuration);

			return services;
		}

		private static IServiceCollection ConfigureProductDbContext(this IServiceCollection services,
			IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnectionString");
			var builder = new MySqlConnectionStringBuilder(connectionString);

			services.AddDbContext<ProductContext>(
				m => m.UseMySql(builder.ConnectionString, ServerVersion.AutoDetect(builder.ConnectionString),
				e =>
				{
					e.MigrationsAssembly("Product.Api");
					e.SchemaBehavior(MySqlSchemaBehavior.Ignore);
				}
				));

			return services;
		}
	}
}
