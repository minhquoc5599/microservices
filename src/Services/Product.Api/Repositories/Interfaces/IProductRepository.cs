using Contract.Common.Interfaces;
using Product.Api.Data;
using Product.Api.Entities;

namespace Product.Api.Repositories.Interfaces
{
	public interface IProductRepository : IRepositoryBase<ProductEntity, long, ProductContext>
	{
		Task<IEnumerable<ProductEntity>> GetProducts();
		Task<ProductEntity> GetProduct(long id);
		Task<ProductEntity> GetProductByNo(string productNo);
		Task CreateProduct(ProductEntity product);
		Task UpdateProduct(ProductEntity product);
		Task DeleteProduct(long id);
	}
}
