using Basket.Api.Entities;
using Basket.Api.Repositories.Interfaces;
using Contract.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.Api.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDistributedCache _cache;
		private readonly ISerializeService _serializeService;
		private readonly ILogger _logger;
		public BasketRepository(IDistributedCache cache, ISerializeService serializeService, ILogger logger)
		{
			_cache = cache;
			_serializeService = serializeService;
			_logger = logger;
		}

		public async Task<Cart?> GetBasketByUserName(string username)
		{
			var check = await _cache.GetStringAsync(username);
			return string.IsNullOrEmpty(check) ? null : _serializeService.Deserialize<Cart>(check);
		}

		public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
		{
			if (options != null)
			{
				await _cache.SetStringAsync(cart.UserName, _serializeService.Serialize(cart), options);
			}
			else
			{
				await _cache.SetStringAsync(cart.UserName, _serializeService.Serialize(cart));
			}
			return await GetBasketByUserName(cart.UserName);
		}
		public async Task<bool> DeleteBasketFromUserName(string username)
		{
			try
			{

				await _cache.RemoveAsync(username);
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error("DeleteBasketFromUserName: ", ex.Message);
				throw;
			}
		}
	}
}
