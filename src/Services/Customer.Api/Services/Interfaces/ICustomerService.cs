namespace Customer.Api.Services.Interfaces
{
	public interface ICustomerService
	{
		Task<IResult> GetCustomerByUserName(string username);
		Task<IResult> GetCustomers();
	}
}
