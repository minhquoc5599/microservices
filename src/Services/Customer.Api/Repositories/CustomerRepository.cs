using Contract.Common.Interfaces;
using Customer.Api.Data;
using Customer.Api.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Repositories
{
	public class CustomerRepository : RepositoryBase<Entities.Customer, int, CustomerContext>, ICustomerRepository
	{
		public CustomerRepository(CustomerContext context,
			IUnitOfWork<CustomerContext> unitOfWork) : base(context, unitOfWork)
		{

		}

		public async Task<Entities.Customer> GetCustomerByUserName(string userName)
		{
			return await FindByCondition(x => x.UserName.Equals(userName)).SingleOrDefaultAsync();
		}

		public async Task<IEnumerable<Entities.Customer>> GetCustomers()
		{
			return await FindAll().ToListAsync();
		}
	}
}
