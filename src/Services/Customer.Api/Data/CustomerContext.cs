﻿using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Data
{
	public class CustomerContext : DbContext
	{
		public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }
		public DbSet<Entities.Customer> Customers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Entities.Customer>().HasIndex(x => x.UserName).IsUnique();
			modelBuilder.Entity<Entities.Customer>().HasIndex(x => x.EmailAddress).IsUnique();
		}
	}
}
