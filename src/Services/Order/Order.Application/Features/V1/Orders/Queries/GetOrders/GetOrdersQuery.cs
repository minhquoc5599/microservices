using MediatR;
using Order.Application.Common.Models;
using Shared.SeedWork;

namespace Order.Application.Features.V1.Orders.Queries.GetOrders
{
	public class GetOrdersQuery : IRequest<ApiResult<List<OrderDto>>>
	{
		public string UserName { get; set; }
		public GetOrdersQuery(string userName)
		{
			UserName = userName ?? throw new ArgumentNullException(nameof(userName));
		}
	}
}
