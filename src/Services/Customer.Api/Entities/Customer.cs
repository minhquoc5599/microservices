using Contract.Domains.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Api.Entities
{
	public class Customer : EntityBase<int>
	{
		[Required]
		public required string UserName { get; set; }

		[Required]
		[Column(TypeName = "varchar(100)")]
		public required string FirstName { get; set; }

		[Required]
		[Column(TypeName = "varchar(150)")]
		public required string LastName { get; set; }

		[Required]
		[EmailAddress]
		public required string EmailAddress { get; set; }
	}
}
