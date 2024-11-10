using Order.Application.Common.Mappings;
using Order.Domain.Entities;

namespace Order.Application.Common.Models
{
	public class OrderDto : IMapFrom<OrderEntity>
	{
		public long Id { get; set; }
		public string UserName { get; set; }
		public decimal TotalPrice { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }

		public string Address { get; set; }
		public string InvoiceAddress { get; set; }
		public string Status { get; set; }

	}
}
