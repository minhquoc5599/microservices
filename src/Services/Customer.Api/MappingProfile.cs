using AutoMapper;
using Shared.Models.Customer;

namespace Customer.Api
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Entities.Customer, CustomerDto>();
		}
	}
}
