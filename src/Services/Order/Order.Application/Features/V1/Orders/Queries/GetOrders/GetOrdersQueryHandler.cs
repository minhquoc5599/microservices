using AutoMapper;
using MediatR;
using Order.Application.Common.Interfaces;
using Order.Application.Common.Models;
using Shared.SeedWork;
using ILogger = Serilog.ILogger;

namespace Order.Application.Features.V1.Orders.Queries.GetOrders
{
	public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
	{
		private readonly IMapper _mapper;
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger _logger;

		public GetOrdersQueryHandler(IMapper mapper, IOrderRepository orderRepository, ILogger logger)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(orderRepository));
		}

		private const string MethodName = "GetOrdersQueryHandler";

		public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
		{
			_logger.Information($"Begin: {MethodName}");
			var orderEntities = await _orderRepository.GetOrdersbyUserName(request.UserName);
			var orderList = _mapper.Map<List<OrderDto>>(orderEntities);

			_logger.Information($"End: {MethodName}");
			return new ApiSuccessResult<List<OrderDto>>(orderList);
		}
	}
}
