using AutoMapper;
using MediatR;
using Order.Application.Common.Interfaces;
using Order.Application.Common.Models;
using Shared.SeedWork;

namespace Order.Application.Features.V1.Orders.Queries.GetOrders
{
	public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
	{
		private readonly IMapper _mapper;
		private readonly IOrderRepository _orderRepository;

		public GetOrdersQueryHandler(IMapper mapper, IOrderRepository orderRepository)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(_orderRepository));
		}

		public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
		{
			var orderEntities = await _orderRepository.GetOrdersbyUserName(request.UserName);
			var orderList = _mapper.Map<List<OrderDto>>(orderEntities);

			return new ApiSuccessResult<List<OrderDto>>(orderList);
		}
	}
}
