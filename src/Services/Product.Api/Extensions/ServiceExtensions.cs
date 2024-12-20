﻿using Contract.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product.Api.Data;
using Product.Api.Repositories;
using Product.Api.Repositories.Interfaces;

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
			services.AddInfrastructureServices();
			services.AddAutoMapper(config => config.AddProfile(new MappingProfile()));

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

		private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
		{
			return services.AddScoped(typeof(IRepositoryBase<,,>), typeof(RepositoryBase<,,>))
				.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
				.AddScoped<IProductRepository, ProductRepository>();
		}
	}
}
