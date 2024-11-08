using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;
using Order.Domain.Enums;

namespace Order.Infrastructure.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
	{
		public void Configure(EntityTypeBuilder<OrderEntity> builder)
		{
			builder.Property(x => x.Status).HasDefaultValue(OrderStatus.New).IsRequired();
		}
	}
}
