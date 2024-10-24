using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Entities;
using Product.Api.Repositories.Interfaces;
using Shared.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _repository;
		private IMapper _mapper;

		public ProductController(IProductRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		#region CRUD

		[HttpGet]
		public async Task<ActionResult> GetProducts()
		{
			var products = await _repository.GetProducts();
			var result = _mapper.Map<IEnumerable<ProductDto>>(products);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetProduct([Required] long id)
		{
			var product = await _repository.GetProduct(id);
			if (product == null)
			{
				return NotFound();
			}
			var result = _mapper.Map<ProductDto>(product);
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult> CreateProduct([FromBody] CreateProductDto request)
		{
			var productEntity = await _repository.GetProductByNo(request.No);
			if (productEntity != null) return BadRequest($"Product No: {request.No} is existed");

			var product = _mapper.Map<ProductEntity>(request);
			await _repository.CreateProduct(product);
			await _repository.SaveChangesAsync();
			var result = _mapper.Map<ProductDto>(product);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateProduct([Required] long id, [FromBody] UpdateProductDto request)
		{
			var product = await _repository.GetProduct(id);
			if (product == null)
			{
				return NotFound();
			}

			var updateProduct = _mapper.Map(request, product);
			await _repository.UpdateProduct(updateProduct);
			await _repository.SaveChangesAsync();

			var result = _mapper.Map<ProductDto>(product);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct([Required] long id)
		{
			var product = await _repository.GetProduct(id);
			if (product == null)
			{
				return NotFound();
			}

			await _repository.DeleteProduct(id);
			await _repository.SaveChangesAsync();
			return NoContent();
		}

		#endregion

		#region Additional Resource

		[HttpGet("get-product-by-no/{productNo}")]
		public async Task<ActionResult> GetProductByNo([Required] string productNo)
		{
			var product = await _repository.GetProductByNo(productNo);
			if (product == null)
			{
				return NotFound();
			}
			var result = _mapper.Map<ProductDto>(product);
			return Ok(result);
		}
		#endregion
	}
}
