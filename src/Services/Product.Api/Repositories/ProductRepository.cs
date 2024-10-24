using Contract.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.Api.Data;
using Product.Api.Entities;
using Product.Api.Repositories.Interfaces;

namespace Product.Api.Repositories
{
	public class ProductRepository : RepositoryBase<ProductEntity, long, ProductContext>, IProductRepository
	{
		public ProductRepository(ProductContext context, IUnitOfWork<ProductContext> unitOfWork)
			: base(context, unitOfWork)
		{
		}

		public async Task<IEnumerable<ProductEntity>> GetProducts()
		{
			return await FindAll().ToListAsync();
		}

		public Task<ProductEntity> GetProduct(long id)
		{
			return GetByIdAsync(id);
		}

		public Task<ProductEntity> GetProductByNo(string productNo)
		{
			return FindByCondition(x => x.No.Equals(productNo)).SingleOrDefaultAsync();
		}

		public Task CreateProduct(ProductEntity product)
		{
			return CreateAsync(product);
		}

		public Task UpdateProduct(ProductEntity product)
		{
			return UpdateAsync(product);
		}

		public async Task DeleteProduct(long id)
		{
			var product = await GetProduct(id);
			if (product != null)
			{
				await DeleteAsync(product);
			}
		}
	}
}
