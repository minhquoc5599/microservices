using Contract.Common.Interfaces;
using Order.Domain.Entities;

namespace Order.Application.Common.Interfaces
{
	public interface IOrderRepository : IRepositoryBase<OrderEntity, long>
	{
		Task<IEnumerable<OrderEntity>> GetOrdersbyUserName(string userName);
	}
}
