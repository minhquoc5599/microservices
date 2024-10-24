using Product.Api.Entities;
using ILogger = Serilog.ILogger;

namespace Product.Api.Data
{
	public class ProductSeeder
	{
		public static async Task SeedProductAsync(ProductContext context, ILogger logger)
		{
			if (!context.Products.Any())
			{
				context.AddRange(getProducts());
				await context.SaveChangesAsync();
				logger.Information("Seeded data for Product Db associated with context {DbContextName}",
					nameof(context));
			}
		}

		private static IEnumerable<ProductDto> getProducts()
		{
			return new List<ProductDto>
			{
				new()
				{
					No = "Honda",
					Name = "Winner",
					Summary= "aaa",
					Description="aaa",
					Price=(decimal)3500.49
				},
				new()
				{
					No = "Yamaha",
					Name = "Exciter",
					Summary= "aaa",
					Description="aaa",
					Price=(decimal)3500.49
				}
			};
		}
	}
}
