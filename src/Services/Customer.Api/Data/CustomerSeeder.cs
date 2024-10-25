using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Data
{
	public static class CustomerSeeder
	{
		public static IHost SeedCustomer(this IHost host)
		{
			using var scope = host.Services.CreateScope();
			var customerContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
			customerContext.Database.MigrateAsync().GetAwaiter().GetResult();

			CreateCustomer(customerContext, "customer1", "customer1", "customer", "customer1@gmail.com")
				.GetAwaiter().GetResult();
			CreateCustomer(customerContext, "customer2", "customer2", "customer", "customer2@gmail.com")
				.GetAwaiter().GetResult();
			return host;
		}

		private static async Task CreateCustomer(CustomerContext context, string username, string firstname,
			string lastname, string email)
		{
			var customer = await context.Customers.SingleOrDefaultAsync(
				x => x.UserName.Equals(username) || x.EmailAddress.Equals(email));
			if (customer == null)
			{
				var newCustomer = new Entities.Customer
				{
					UserName = username,
					FirstName = firstname,
					LastName = lastname,
					EmailAddress = email
				};
				await context.Customers.AddAsync(newCustomer);
				await context.SaveChangesAsync();
			}
		}
	}
}
