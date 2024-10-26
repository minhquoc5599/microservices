using Contract.Common.Interfaces;
using Customer.Api.Data;

namespace Customer.Api.Repositories.Interfaces
{
	public interface ICustomerRepository : IRepositoryBase<Entities.Customer, int, CustomerContext>
	{
		Task<Entities.Customer> GetCustomerByUserName(string userName);
		Task<IEnumerable<Entities.Customer>> GetCustomers();
	}
}
