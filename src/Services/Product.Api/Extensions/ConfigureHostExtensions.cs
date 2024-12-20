﻿namespace Product.Api.Extensions
{
	public static class ConfigureHostExtensions
	{
		public static void AddAppConfigurations(this ConfigureHostBuilder builder) {
			builder.ConfigureAppConfiguration((context, config) =>
			{
				var env = context.HostingEnvironment;
				config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();
			});
		}
	}
}
