using AutoMapper;
using Customer.Api.Repositories.Interfaces;
using Customer.Api.Services.Interfaces;
using Shared.Models.Customer;

namespace Customer.Api.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _repository;
		private readonly IMapper _mapper;

		public CustomerService(ICustomerRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IResult> GetCustomerByUserName(string username)
		{
			var customer = await _repository.GetCustomerByUserName(username);
			var result = _mapper.Map<CustomerDto>(customer);
			return Results.Ok(result);
		}

		public async Task<IResult> GetCustomers()
		{
			var customers = await _repository.GetCustomers();
			var result = customers.Select(x => _mapper.Map<CustomerDto>(x)).ToList();
			return Results.Ok(result);
		}
	}
}
