﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Common.Models;
using Order.Application.Features.V1.Orders.Queries.GetOrders;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Order.Api.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public OrderController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		private static class RouteNames
		{
			public const string GetOrders = nameof(GetOrders);
		}

		[HttpGet("{username}", Name = RouteNames.GetOrders)]
		[ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([Required] string username)
		{
			var query = new GetOrdersQuery(username);
			var result = await _mediator.Send(query);
			return Ok(result);
		}
	}
}
