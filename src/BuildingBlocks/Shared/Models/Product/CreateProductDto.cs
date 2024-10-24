using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Product
{
    public class CreateProductDto: CreateOrUpdateProductDto
    {
        [Required]
        public string No { get; set; }
    }
}
