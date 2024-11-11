using Contract.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Order.Application.Common.Interfaces;
using Order.Domain.Entities;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories
{
	public class OrderRepository : RepositoryBase<OrderEntity, long, OrderContext>, IOrderRepository
	{
		public OrderRepository(OrderContext context, IUnitOfWork<OrderContext> unitOfWork) : base(context, unitOfWork)
		{
		}

		public async Task<IEnumerable<OrderEntity>> GetOrdersbyUserName(string userName)
		{
			return await FindByCondition(x => x.UserName.Equals(userName)).ToListAsync();
		}
	}
}
