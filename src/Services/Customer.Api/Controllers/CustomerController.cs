using Customer.Api.Repositories.Interfaces;
using Customer.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Controllers
{
	public static class CustomerController
	{
		public static void MapCustomerApi(this WebApplication app)
		{
			app.MapGet("/api/customer", async (ICustomerService service) => await service.GetCustomers());

			app.MapGet("/api/customer/{username}", async (string username, ICustomerService service) =>
			{
				var customer = await service.GetCustomerByUserName(username);
				return customer != null ? customer : Results.NotFound();
			});

			app.MapPost("/api/customer", async (Entities.Customer customer,
				ICustomerRepository repository) =>
				{
					await repository.CreateAsync(customer);
					await repository.SaveChangesAsync();
				});

			//app.MapPut("/api/customer", () => "Welcome to Customer Api!");

			app.MapDelete("/api/customer/{id}", async (int id, ICustomerRepository repository) =>
			{
				var customer = await repository.FindByCondition(x => x.Id.Equals(id)).SingleOrDefaultAsync();
				if (customer == null) return Results.NotFound();

				await repository.DeleteAsync(customer);
				await repository.SaveChangesAsync();
				return Results.NoContent();
			});
		}
	}
}
