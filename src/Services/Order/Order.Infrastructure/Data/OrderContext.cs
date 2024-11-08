using Contract.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using System.Reflection;

namespace Order.Infrastructure.Data
{
	public class OrderContext : DbContext
	{
		public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

		public DbSet<OrderEntity> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var modified = ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Added ||
				e.State == EntityState.Modified ||
				e.State == EntityState.Deleted);

			foreach (var entry in modified)
			{
				switch (entry.State)
				{
					case EntityState.Added:
						if (entry.Entity is IDateTracking addedEntity)
						{
							addedEntity.CreatedDate = DateTime.UtcNow;
							entry.State = EntityState.Added;
						}
						break;

					case EntityState.Modified:
						Entry(entry.Entity).Property("Id").IsModified = false;
						if (entry.Entity is IDateTracking modifiedEntity)
						{
							modifiedEntity.LastModifiedDate = DateTime.UtcNow;
							entry.State = EntityState.Modified;
						}
						break;
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}

	}
}
