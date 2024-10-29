using Basket.Api.Repositories;
using Basket.Api.Repositories.Interfaces;
using Contract.Common.Interfaces;
using Infrastructure.Common;

namespace Basket.Api.Extensions
{
	public static class ServiceExtension
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			services.AddScoped<IBasketRepository, BasketRepository>()
				.AddTransient<ISerializeService, SerializeService>();
			return services;
		}

		public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
		{
			var redisConnectionString = configuration.GetSection("CacheSettings:ConnectionString").Value;
			if (string.IsNullOrEmpty(redisConnectionString))
			{
				throw new ArgumentNullException("Redis connection string is not configured!");
			}

			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = redisConnectionString;
			});
		}
	}


}
