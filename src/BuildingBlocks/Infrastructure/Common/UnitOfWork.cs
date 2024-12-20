﻿using Contract.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
	public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
	{
		private readonly TContext _context;

		public UnitOfWork(TContext context)
		{
			_context = context;
		}

		public Task<int> CommitAsync()
		{
			return _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
