using Customer.Api.Repositories.Interfaces;
using Customer.Api.Services.Interfaces;

namespace Customer.Api.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _repository;

		public CustomerService(ICustomerRepository repository)
		{
			_repository = repository;
		}

		public async Task<IResult> GetCustomerByUserName(string username)
		{
			return Results.Ok(await _repository.GetCustomerByUserName(username));
		}

		public async Task<IResult> GetCustomers()
		{
			return Results.Ok(await _repository.GetCustomers());
		}
	}
}
