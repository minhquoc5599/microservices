using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using ILogger = Serilog.ILogger;


namespace Order.Infrastructure.Data
{
	public class OrderSeeder
	{
		private readonly OrderContext _context;
		private readonly ILogger _logger;

		public OrderSeeder(OrderContext context, ILogger logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task InitializeAsync()
		{
			try
			{
				if (_context.Database.IsSqlServer())
				{
					await _context.Database.MigrateAsync();
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while initializing the database!");
			}
		}

		public async Task SeedOrderAsync()
		{
			if (!_context.Orders.Any())
			{
				await _context.Orders.AddRangeAsync(new OrderEntity
				{
					UserName = "customer1",
					FirstName = "customer1",
					LastName = "customer",
					EmailAddress = "customer1@gmail.com",
					Address = "Hcm",
					InvoiceAddress = "Hcm",
					TotalPrice = 250
				});
				await _context.SaveChangesAsync();
				_logger.Information("Seeded data for Order Db");
			}
		}
	}
}
