﻿using Basket.Api.Entities;
using Basket.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Basket.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;

		public BasketController(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}

		[HttpGet("{username}", Name = "GetBasket")]
		[ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<Cart>> GetBasketByUserName([Required] string username)
		{
			var result = await _basketRepository.GetBasketByUserName(username);
			return Ok(result ?? new Cart());
		}

		[HttpPost(Name = "UpdateBasket")]
		[ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<Cart>> UpdateBasket([FromBody] Cart cart)
		{
			var options = new DistributedCacheEntryOptions()
				.SetAbsoluteExpiration(DateTime.UtcNow.AddHours(1))
				.SetSlidingExpiration(TimeSpan.FromMinutes(5));

			var result = await _basketRepository.UpdateBasket(cart, options);
			return Ok(result);
		}

		[HttpDelete("{username}", Name = "DeleteBasket")]
		[ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<bool>> DeleteBasket([Required] string username)
		{
			var result = await _basketRepository.DeleteBasketFromUserName(username);
			return Ok(result);
		}
	}
}
