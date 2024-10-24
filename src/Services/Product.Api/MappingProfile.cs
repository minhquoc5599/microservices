using AutoMapper;
using Infrastructure.Extensions;
using Product.Api.Entities;
using Shared.Models.Product;

namespace Product.Api
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ProductEntity, ProductDto>();
			CreateMap<CreateProductDto, ProductEntity>();
			CreateMap<UpdateProductDto, ProductEntity>().IgnoreAllNonExisting();
		}
	}
}
