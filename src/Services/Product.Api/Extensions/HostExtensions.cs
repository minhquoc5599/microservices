﻿using Microsoft.EntityFrameworkCore;

namespace Product.Api.Extensions
{
	public static class HostExtensions
	{
		public static IHost MigrationDatabase<TContext>(this IHost host,
			Action<TContext, IServiceProvider> seeder) where TContext : DbContext
		{
			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var logger = services.GetRequiredService<ILogger<TContext>>();
				var context = services.GetService<TContext>();

				try
				{
					logger.LogInformation("Migrating mysql database");
					ExcuteMigrations(context);
					logger.LogInformation("Migrated mysql database");
					InvokeSeeder(seeder, context, services);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "Error migrating mysql database");
				}
			}
			return host;
		}

		private static void ExcuteMigrations<TContext>(TContext context) where TContext : DbContext
		{
			context.Database.Migrate();
		}

		private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
			IServiceProvider services) where TContext : DbContext
		{
			seeder(context, services);
		}
	}
}
